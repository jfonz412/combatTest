using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStatusController : MonoBehaviour
{
    /*
    private Brain myBrain;
    private BodyPartController body;
    private UnitAnimController anim;
    private CombatSkills combatSkills;
    private IEnumerator shock;
    private IEnumerator suffocation;

    private int baseResistance = 100; //got rid of this in BodyPart.cs, willpower will be enough once i balance skills

    //private Dictionary<Status, string> statusMessages; //will be used to send lines to the battle reports
    //if Dead, cancel all status coroutines? might not be necessary

    private void Start()
    {
        myBrain = GetComponent<Brain>();
        body = GetComponent<BodyPartController>();
        combatSkills = GetComponent<CombatSkills>();
        anim = GetComponent<UnitAnimController>();
    }

    public void CheckForStatusTriggers(BodyPartController.DamageInfo info)
    {
        combatSkills.ExperienceGain(CombatSkills.CombatSkill.Willpower, 20f);
        BodyPartController.Parts p = info.bodyPart;
        int s = info.severityLevel;

        Bleed(p, s);
        CantBreathe(p, s);

        if (!myBrain.ActiveState(Brain.State.Unconscious))
        {
            Vomit(p, s);

            if (!myBrain.ActiveState(Brain.State.Downed))
                FallDown(p, s);

            if (p == BodyPartController.Parts.Head)
            {
                Rocked(p, s);
                KnockedOut(p, s);
            }

            if (!myBrain.ActiveState(Brain.State.Shock) && !myBrain.ActiveState(Brain.State.CantBreathe))
            {
                if (body.OverallHealth() * 100 < 65)
                {
                    OverWhelmed(p, s);
                }
            }
        }
    }

    #region Statuses for many bodyparts

    private void Bleed(BodyPartController.Parts part, int severityLevel)
    {
        if (severityLevel >= BodyPartDamageLimits.partBleedLimits[part])
            StartCoroutine(Bleeding(severityLevel));
    }

    private void SlipIntoShock()
    {
        if (shock == null) //WILL BE CLEARED WHEN THE COROUTINE COMPLETES
        {
            string line = "<color=red>" + gameObject.name + " is experiencing shock from loss of blood!</color>";
            BattleReport.AddToBattleReport(line);

            myBrain.ToggleState(Brain.State.Shock, true);
            anim.Shock();

            shock = Shock();
            StartCoroutine(shock);
        }
    }

    protected IEnumerator Bleeding(int severity)
    {
        //using 20 here because 12 parts * 5 severity = 1200 (roughly, this gets inaacurate with creatures with more/less parts)
        float bloodNeeded = 600; //half of 1200 which I've set as a hardcoded default blood volume for all units
        float damage = severity * 20;

        while (damage > 0 && body.totalBlood > 0)
        {
            body.totalBlood -= damage;
            damage = (damage / 2) - 1f;
            //Debug.Log(gameObject.name + " is bleeding for " + damage + " damage");
            yield return new WaitForSeconds(1f);
        }

        if (body.totalBlood <= bloodNeeded)
        {
            SlipIntoShock();
        }

        yield break;
    }

    protected IEnumerator Shock()
    {
        float bloodNeeded = 600; //half of 1200 which I've set as a hardcoded default blood volume for all units
        float deathTimer = 20f;


        while (body.totalBlood < bloodNeeded)
        {
            deathTimer -= Time.deltaTime;
            Debug.Log(deathTimer);
            if (deathTimer <= 0)
            {
                string line = "<color=red>" + gameObject.name + " has bled out!</color>";
                BattleReport.AddToBattleReport(line);
                StopAllCoroutines(); //need this in case we are suffocating and die this way first
                myBrain.Die();
                yield break;
            }
            yield return null;
        }

        //if we've exited the while loop before the timer is up then we've re-accumulated the lost blood and can exit shock
        myBrain.ToggleState(Brain.State.Shock, false);
        shock = null;
        yield break;
    }

    private void FallDown(BodyPart part, int severityLevel)
    {
        /*
        if(severityLevel >= BodyPartDamageLimits.partDownedLimits[part])
        {
            if (Random.Range(0, 100) > combatSkills.statusResistance + baseResistance / (severityLevel + 1))
            {
                myBrain.TriggerTemporaryState(Brain.State.Downed, severityLevel);
                anim.FallOver();
                string line = "<color=blue>" + gameObject.name + " is knocked to the ground!</color>";
                BattleReport.AddToBattleReport(line);
            }
        }
    }

    private void Vomit(BodyPartController.Parts part, int severityLevel)
    {
        if (severityLevel >= BodyPartDamageLimits.partVomitLimits[part])
        {
            if (Random.Range(0, 100) > combatSkills.statusResistance + baseResistance / (severityLevel + 1))
            {
                myBrain.TriggerTemporaryState(Brain.State.Vomitting, severityLevel);
                anim.Vomit(); //ANIM MUST COME AFTER STATE IS FLIPPED
                string line = "<color=green>The injury causes " + gameObject.name + " to vomit!</color>";
                BattleReport.AddToBattleReport(line);
            }
        }
    }

    private void CantBreathe(BodyPartController.Parts part, int severityLevel)
    {
        if (severityLevel >= BodyPartDamageLimits.partCantBreatheLimits[part])
        {
            if (severityLevel >= 4)
            {
                Suffocate();
                return;
            }

            if (Random.Range(0, 100) > combatSkills.statusResistance + baseResistance / (severityLevel + 1))
            {
                myBrain.TriggerTemporaryState(Brain.State.CantBreathe, severityLevel);
                anim.CantBreath();
                string line = "<color=blue>" + gameObject.name + " is struggling to breathe!</color>";
                BattleReport.AddToBattleReport(line);
            }
        }
    }

    private void Suffocate()
    {
        if(suffocation == null)
        {
            suffocation = Suffocating();
            StartCoroutine(suffocation);
        }
    }

    private IEnumerator Suffocating()
    {
        //float airNeeded = 60f; 
        float deathTimer = 20f;

        myBrain.ToggleState(Brain.State.CantBreathe, true);
        string line = "<color=red>" + gameObject.name + " is suffocating to death!" + "</color>";
        BattleReport.AddToBattleReport(line);
        anim.Suffocation();

        while (deathTimer > 0) //AIR NOT IMPLEMENTED
        {
            deathTimer -= Time.deltaTime;
            Debug.Log(deathTimer);
            if (deathTimer <= 0)
            {
                line = "<color=red>" + gameObject.name + " has suffocated to death!" + "</color>";
                BattleReport.AddToBattleReport(line);
                StopAllCoroutines(); //need this in case we are in shock and die this way first
                myBrain.Die();
                yield break;
            }
            yield return null;
        }

        //if we've exited the while loop before the timer is up then we've re-accumulated the lost air and can exit suffocation
        myBrain.ToggleState(Brain.State.CantBreathe, false);
        suffocation = null;
        yield break;
    }
    #endregion

    #region Head Only

    private void Rocked(BodyPartController.Parts part, int severityLevel)
    {
        int triggerLimit = 2;
        if(severityLevel >= triggerLimit)
        {
            if(Random.Range(0,100) > combatSkills.statusResistance + baseResistance / (severityLevel + 1))
            {
                myBrain.TriggerTemporaryState(Brain.State.Rocked, severityLevel);
                anim.Rocked();
                string line = "<color=blue>" + gameObject.name + " was rocked by the attack!</color>";
                BattleReport.AddToBattleReport(line);
            }
        }
    }

    private void KnockedOut(BodyPartController.Parts part, int severityLevel)
    {
        int triggerLimit = 2;
        int multiplier = 10; // * severityLevel to get time knocked out

        if (severityLevel >= triggerLimit)
        {
            if (Random.Range(0, 100) > combatSkills.statusResistance + baseResistance / (severityLevel + 1))
            {
                int duration = severityLevel * multiplier;

                myBrain.TriggerTemporaryState(Brain.State.Unconscious, duration);
                anim.KnockedOut();
                string line = "<color=magenta>" + gameObject.name + " has been knocked unconscious by the attack!</color>";
                BattleReport.AddToBattleReport(line);
            }
        }
    }

    #endregion

    //this stuff is more AI personality related, caused by overall body health after an attack (ie being "tipped over the edge")
    #region Overwhelmed
    private void OverWhelmed(BodyPartController.Parts p, int s)
    {
        if (OvercomeByAnger(p, s))
            return;
        if (OvercomeByFear(p, s))
            return;
        if (OvercomeByPain(p, s))
            return;
    }

    private bool OvercomeByFear(BodyPartController.Parts part, int severityLevel)
    {
        if (Random.Range(0, 100) > combatSkills.statusResistance + baseResistance / (severityLevel + 1))
        {
            string line = "<color=yellow>" + gameObject.name + " is overcome by fear!</color>";
            BattleReport.AddToBattleReport(line);
            return true;
        }
        return false;
    }

    private bool OvercomeByPain(BodyPartController.Parts part, int severityLevel)
    {
        if (Random.Range(0, 100) > combatSkills.statusResistance + baseResistance / (severityLevel + 1))
        {
            string line = "<color=yellow>" + gameObject.name + " is overcome by pain!</color>";
            BattleReport.AddToBattleReport(line);
            return true;
        }
        return false;
    }

    private bool OvercomeByAnger(BodyPartController.Parts part, int severityLevel)
    {
        if (Random.Range(0, 100) > combatSkills.statusResistance + baseResistance / (severityLevel + 1))
        {
            string line = "<color=yellow>" + gameObject.name + " is overcome by anger!</color>";
            BattleReport.AddToBattleReport(line);
            return true;
        }
        return false;
    }
#endregion
*/
}
