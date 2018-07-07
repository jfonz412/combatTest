using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStatusController : MonoBehaviour
{
    private Brain myBrain;
    private BodyParts body;
    private CombatSkills combatSkills;

    private int baseResistance = 100;

    //private Dictionary<Status, string> statusMessages; //will be used to send lines to the battle reports

    private void Start()
    {
        myBrain = GetComponent<Brain>();
        body = GetComponent<BodyParts>();
        combatSkills = GetComponent<CombatSkills>();
    }

    public void CheckForStatusTriggers(BodyParts.DamageInfo info)
    {
        combatSkills.ExperienceGain(CombatSkills.CombatSkill.Willpower, 20f);
        BodyParts.Parts p = info.bodyPart;
        int s = info.severityLevel;

        Bleed(p, s);
        FallDown(p, s);
        Vomit(p, s);
        CantBreathe(p, s);

        if (p == BodyParts.Parts.Head)
        {
            Rocked(p, s);
            KnockedOut(p, s);
        }

        if(body.OverallHealth() * 100 < 65)
        {
            OverWhelmed(p, s);
        }
    }


    #region Statuses for many bodyparts

    private void Bleed(BodyParts.Parts part, int severityLevel)
    {
        if (severityLevel >= BodyPartDamageLimits.partBleedLimits[part])
            StartCoroutine(Bleeding(severityLevel));
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
            Debug.Log(gameObject.name + " is bleeding for " + damage + " damage");
            yield return new WaitForSeconds(1f);
        }

        if (body.totalBlood <= bloodNeeded)
        {
            SlipIntoShock();
        }

        yield break;
    }

    private void SlipIntoShock()
    {

        string line = gameObject.name + " is experiencing shock from loss of blood!";
        BattleReport.AddToBattleReport(line);

        myBrain.ToggleState(Brain.State.Shock, true); 
    }

    private void FallDown(BodyParts.Parts part, int severityLevel)
    {
        if(severityLevel >= BodyPartDamageLimits.partDownedLimits[part])
        {
            if (Random.Range(0, 100) > combatSkills.statusResistance + baseResistance / (severityLevel + 1))
            {
                myBrain.TriggerTemporaryState(Brain.State.Downed, severityLevel);
                string line = gameObject.name + " is knocked to the ground!";
                BattleReport.AddToBattleReport(line);
            }
        }
    }

    private void Vomit(BodyParts.Parts part, int severityLevel)
    {
        if (severityLevel >= BodyPartDamageLimits.partVomitLimits[part])
        {
            if (Random.Range(0, 100) > combatSkills.statusResistance + baseResistance / (severityLevel + 1))
            {
                myBrain.TriggerTemporaryState(Brain.State.Vomitting, severityLevel);
                string line = "The injury causes " + gameObject.name + " to vomit!";
                BattleReport.AddToBattleReport(line);
            }
        }
    }

    private void CantBreathe(BodyParts.Parts part, int severityLevel)
    {
        if (severityLevel >= BodyPartDamageLimits.partCantBreatheLimits[part])
        {
            if (Random.Range(0, 100) > combatSkills.statusResistance + baseResistance / (severityLevel + 1))
            {
                myBrain.TriggerTemporaryState(Brain.State.CantBreathe, severityLevel);
                string line = gameObject.name + " is struggling to breathe!";
                BattleReport.AddToBattleReport(line);
            }
        }
    }
    #endregion

    #region Head Only

    private void Rocked(BodyParts.Parts part, int severityLevel)
    {
        int triggerLimit = 2;
        if(severityLevel >= triggerLimit)
        {
            if(Random.Range(0,100) > combatSkills.statusResistance + baseResistance / (severityLevel + 1))
            {
                myBrain.TriggerTemporaryState(Brain.State.Rocked, severityLevel);
                string line = gameObject.name + " was rocked by the attack!";
                BattleReport.AddToBattleReport(line);
            }
        }
    }

    private void KnockedOut(BodyParts.Parts part, int severityLevel)
    {
        int triggerLimit = 2;
        int multiplier = 10; // * severityLevel to get time knocked out

        if (severityLevel >= triggerLimit)
        {
            if (Random.Range(0, 100) > combatSkills.statusResistance + baseResistance / (severityLevel + 1))
            {
                myBrain.TriggerTemporaryState(Brain.State.Unconscious, severityLevel * multiplier);
                string line = gameObject.name + " has been knocked unconscious by the attack!";
                BattleReport.AddToBattleReport(line);
            }
        }
    }

    #endregion

    //this stuff is more AI personality related, caused by overall body health after an attack (ie being "tipped over the edge")
    #region Overwhelmed
    private void OverWhelmed(BodyParts.Parts p, int s)
    {
        if (OvercomeByAnger(p, s))
            return;
        if (OvercomeByFear(p, s))
            return;
        if (OvercomeByPain(p, s))
            return;
    }

    private bool OvercomeByFear(BodyParts.Parts part, int severityLevel)
    {
        if (Random.Range(0, 100) > combatSkills.statusResistance + baseResistance / (severityLevel + 1))
        {
            string line = gameObject.name + " is overcome by fear!";
            BattleReport.AddToBattleReport(line);
            return true;
        }
        return false;
    }

    private bool OvercomeByPain(BodyParts.Parts part, int severityLevel)
    {
        if (Random.Range(0, 100) > combatSkills.statusResistance + baseResistance / (severityLevel + 1))
        {
            string line = gameObject.name + " is overcome by pain!";
            BattleReport.AddToBattleReport(line);
            return true;
        }
        return false;
    }

    private bool OvercomeByAnger(BodyParts.Parts part, int severityLevel)
    {
        if (Random.Range(0, 100) > combatSkills.statusResistance + baseResistance / (severityLevel + 1))
        {
            string line = gameObject.name + " has become enraged!";
            BattleReport.AddToBattleReport(line);
            return true;
        }
        return false;
    }
#endregion
}
