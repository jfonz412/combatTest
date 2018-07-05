using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStatusController : MonoBehaviour
{
    private UnitController movement;
    private UnitReactions ai;
    private BodyParts body;
    private CombatSkills combatSkills;
    private AttackController attackController;

    public enum Status { Normal, Shock, CantBreathe, OvercomeByPain, Unconscious, Vomitting, Downed, Rocked, OvercomeByFear, OvercomeByRage }
    private Dictionary<Status, string> statusMessages; //will be used to send lines to the battle reports

    //will need to be a little more specific, for example a strong impact to 
    //the belly will knock the wind out of you, as will puncturing the lungs or crushing the windpipe
    private Status[] headStates = new Status[] { Status.Unconscious, Status.Rocked, Status.Vomitting, Status.Downed, Status.OvercomeByFear, Status.OvercomeByPain, Status.OvercomeByRage };
    private Status[] bodyStates = new Status[] { Status.Shock, Status.Vomitting, Status.Downed, Status.OvercomeByFear, Status.OvercomeByPain, Status.OvercomeByRage };

    //private Status currentStatus = Status.Normal;
    private int baseResistance = 100;

    // Use this for initialization
    void Start()
    {
        movement = GetComponent<UnitController>();
        ai = GetComponent<UnitReactions>();
        body = GetComponent<BodyParts>();
        combatSkills = GetComponent<CombatSkills>();
        attackController = GetComponent<AttackController>();
    }

    public void CheckForStatusTrigger(BodyParts.DamageInfo info)
    {
        combatSkills.ExperienceGain(CombatSkills.CombatSkill.Willpower, 100f);

        if (info.bodyPart == BodyParts.Parts.Head)
        {
            TriggerStatus(HeadStatus(info.severityLevel));
        }
        else
        {
            TriggerStatus(BodyStatus(info.severityLevel));
        }
    }

    //not currently used, but I'm thinking this will check on the unit even when not in combat to determine if they should maybe pass out or something?
    //not totally sure I will use this
    public void CheckOverallHealth()
    {
        float overallHealth = body.OverallHealth();
        //if health is too low, may trigger overcoming by pain, fear, rage, vomitting, falling, unconsciousness
    }

    private Status HeadStatus(int severityLevel)
    {
        int resistance = combatSkills.statusResistance;
        severityLevel++;
        if (Random.Range(0, 100) > resistance + (baseResistance / severityLevel))
        {
            int n = Random.Range(0, headStates.Length);

            return headStates[n];
        }

        return Status.Normal;
    }

    private Status BodyStatus(int severityLevel)
    {
        int resistance = combatSkills.statusResistance;
        severityLevel++;
        if (Random.Range(0, 100) > resistance + (baseResistance / severityLevel))
        {
            int n = Random.Range(0, bodyStates.Length);

            return bodyStates[n];
        }

        return Status.Normal;
    }

    private void TriggerStatus(Status status)
    {
        switch (status)
        {
            case Status.Normal:
                return;
            case Status.Downed:
                FallDown();
                break;
            case Status.OvercomeByFear:
                OvercomeByFear();
                break;
            case Status.OvercomeByPain:
                OvercomeByPain();
                break;
            case Status.OvercomeByRage:
                Enrage();
                break;
            case Status.Rocked:
                Rocked();
                break;
            case Status.Shock:
                Shock();
                break;
            case Status.Unconscious:
                KnockedOut();
                break;
            case Status.Vomitting:
                Vomit();
                break;
            case Status.CantBreathe:
                CantBreathe();
                break;
            default:
                Debug.LogError("Invalid Status");
                break;
        }
    }

    private void FallDown()
    {
        string line = gameObject.name + " is knocked to the ground!";
        BattleReport.AddToBattleReport(line);
    }

    private void OvercomeByFear()
    {
        string line = gameObject.name + " is overcome by fear!";
        BattleReport.AddToBattleReport(line);
    }

    private void OvercomeByPain()
    {
        string line = gameObject.name + " is overcome by pain!";
        BattleReport.AddToBattleReport(line);
    }

    private void Enrage()
    {
        string line = gameObject.name + " has become enraged!";
        BattleReport.AddToBattleReport(line);
    }

    private void Rocked()
    {
        string line = gameObject.name + " was rocked by the attack!";
        BattleReport.AddToBattleReport(line);
    }

    private void Shock()
    {
        string line = gameObject.name + " is in shock!";
        BattleReport.AddToBattleReport(line);
    }

    private void KnockedOut()
    {
        string line = gameObject.name + " has been knocked unconscious by the attack!";
        BattleReport.AddToBattleReport(line);
    }

    private void Vomit()
    {
        string line =  "The injury causes " + gameObject.name + " to vomit!";
        BattleReport.AddToBattleReport(line);
    }

    private void CantBreathe()
    {
        string line = gameObject.name + " is struggling to breathe!";
        BattleReport.AddToBattleReport(line);
    }
}
