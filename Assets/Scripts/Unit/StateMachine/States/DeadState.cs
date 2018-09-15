using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : State {
    public GameObject deathItem;

    protected override void Init()
    {
        base.Init();
        canTransitionInto = new UnitStateMachine.UnitState[]
        {
            //UnitStateMachine.UnitState.FightOrFlight, //will need this eventually for insta-deaths
            //UnitStateMachine.UnitState.Incapacitated
        };
    }

    protected override void OnStateEnter()
    {
        base.OnStateEnter();
        StartCoroutine(DeathProcess());
    }

    protected override void OnStateExit()
    {
        base.OnStateExit();
    }

    private IEnumerator DeathProcess()
    {
        DieAnim();
        DropDeathItem();
        yield return new WaitForSeconds(6f); //extend death animation before destroy
        gameObject.SetActive(false);
    }

    public void DieAnim()
    {
        //Debug.LogError("Animating death");
        Color color = Color.grey;
        color.a = color.a * 0.75f;
        stateMachine.spriteRend.color = color;
        stateMachine.anim.SetBool("isDown", true);
    }

    private void DropDeathItem()
    {
        if (deathItem != null)
        {
            Instantiate(deathItem, transform.position, Quaternion.identity);
        }
    }
}
