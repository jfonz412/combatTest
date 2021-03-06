﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncapacitatedState : State {
    public enum TemporaryState { InShock, CantBreathe, Suffocating, Vomitting, Dazed, Downed, Unconscious }
    UnitStateMachine usm;
    IEnumerator waiting;
    int coroutineCount = 0;
    bool currentlyFlashing = false;

    # region Main Methods
    protected override void Init()
    {
        base.Init();
        usm = (UnitStateMachine)stateMachine;

        canTransitionInto = new StateMachine.States[]
        {
            StateMachine.States.Dead,
            StateMachine.States.Incapacitated, //need to be able to call this state again if we get status'd again
            StateMachine.States.Idle //if we leave incapacitated state we go back to idle and wait if someone hits us again, in 
            //which case we will enter fight or flight and do another courage check. not a perfect solution if we want the unit to run away as 
            //soon as they can but it's something
        };
    }

    //used to start the masterTimer for this state
    protected override void OnStateEnter()
    {
        base.OnStateEnter();

        if(waiting == null)
        {
            waiting = WaitForCoroutines();
            StartCoroutine(waiting);
        }  
    }

    private IEnumerator WaitForCoroutines()
    {
        //when master timer hits 0
        while (coroutineCount > 0)
        {
            yield return null;
        }
        Debug.Log("exiting incapacitated state");
        
        stateMachine.RequestChangeState(StateMachine.States.Idle);
    }

    protected override void OnStateExit()
    {
        base.OnStateExit();
        StopAllCoroutines();
        waiting = null;
    }

    #endregion

    #region Temp State Logic

    //will be used to trigger this state from UnitStateMachine
    public void Incapacitate(TemporaryState tempState, float duration)
    {
        switch (tempState)
        {
            case TemporaryState.Downed:
                StartCoroutine(Downed(duration));
                Debug.Log(gameObject + " DOWNED");
                break;
            case TemporaryState.Dazed:
                StartCoroutine(Flash(Color.magenta, duration));
                Debug.Log(gameObject + " DAZED");
                break;
            case TemporaryState.CantBreathe:
                StartCoroutine(Flash(Color.blue, duration));
                Debug.Log(gameObject + " CANTBREATHE(" + duration +")");
                break;
            case TemporaryState.Vomitting:
                StartCoroutine(Flash(Color.green, duration));
                Debug.Log(gameObject + " VOMITING");
                break;
            case TemporaryState.InShock:
                StartCoroutine(Shock(duration));
                StartCoroutine(Flash(Color.red, duration));
                StartCoroutine(Downed(duration));
                Debug.Log(gameObject + " IN SHOCK");
                break;
            case TemporaryState.Unconscious:
                StartCoroutine(Flash(Color.magenta, duration));
                StartCoroutine(Downed(duration, true));
                Debug.Log(gameObject + " UNCONSCIOUS");
                break;
            case TemporaryState.Suffocating:
                StartCoroutine(Suffocating(duration));
                StartCoroutine(Flash(Color.blue, 30f));
                StartCoroutine(Downed(duration));
                Debug.Log(gameObject + " SUFFOCATING");
                break;
            default:
                Debug.LogError("TEMPORARY STATE NOT FOUND");
                break;
        }
        OnStateEnter();
    }

    private IEnumerator Downed(float duration, bool knockedOut = false)
    {
        BodyPartController b = usm.bodyController;

        if (knockedOut) //if we were knocked out by this attack, put them down and set unconscious = true
        {
            b.unconscious = true;

            Animator a = stateMachine.anim;
            coroutineCount++;

            while (duration > 0)
            {
                duration -= Time.deltaTime;
                a.SetBool("isDown", true);
                yield return null;
            }
            a.SetBool("isDown", false);
            coroutineCount--;

            b.unconscious = false;
        }
        //otherwise if unit knockdown is triggered (but not knockedOut), see if I'm not already down or unconscious
        else if (!b.unconscious) 
        {
            Animator a = stateMachine.anim;
            coroutineCount++;

            while (duration > 0)
            {
                duration -= Time.deltaTime;
                a.SetBool("isDown", true);
                yield return null;
            }

            a.SetBool("isDown", false);
            coroutineCount--;
        }      
    }

    IEnumerator Flash(Color color, float duration)
    {
        SpriteRenderer s = stateMachine.spriteRend;
        coroutineCount++;
        //Debug.Log("entering flash: " + Time.time);
        while (duration > 0)
        {
            while (color.a > 0.5 && duration > 0)
            {
                color.a -= 0.1f;
                s.color = color;
                yield return new WaitForSeconds(0.1f);
                duration -= 0.1f;
                //Debug.Log("flash duration1: " + duration);
            }
            while (color.a < 1 && duration > 0)
            {
                color.a += 0.1f;
                s.color = color;
                yield return new WaitForSeconds(0.1f);
                duration -= 0.1f;
                //Debug.Log("flash duration2: " + duration);
            }
        
        }
        //Debug.Log("exiting flash: " + Time.time);
        s.color = Color.white;
        coroutineCount--;
    }

    private IEnumerator Shock(float deathTimer)
    {
        float bloodNeeded = 600; //half of 1200 which I've set as a hardcoded default blood volume for all units
        coroutineCount++;

        while (usm.bodyController.totalBlood < bloodNeeded)
        {
            deathTimer -= Time.deltaTime;
            Debug.Log(deathTimer);
            if (deathTimer <= 0)
            {
                string line = "<color=red>" + gameObject.name + " has bled out!</color>";
                BattleReport.AddToBattleReport(line);
                Die(); 
                //Debug.LogError("dying of shock");
                yield break;
            }
            yield return null;
        }
        coroutineCount--;
        yield break;
    }

    private IEnumerator Suffocating(float deathTimer)
    {
        coroutineCount++;       //(while part is still damaged)
        while (deathTimer > 0) //part.SeverityLevel() >= part.SuffocationThreshold() //will need to give BodyPartController a BodyPart suffocatedPart;
        {
            deathTimer -= Time.deltaTime;
            yield return null;
        }
        string line = "<color=red>" + gameObject.name + " has suffocated to death!" + "</color>";
        BattleReport.AddToBattleReport(line);
        Die();

        coroutineCount--; // never going to escape the while loop in this case
        yield break;
    }

    public void Die()
    {
        stateMachine.RequestChangeState(StateMachine.States.Dead);
    }

    #endregion
}
