using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Collections;

public class BodyPartController : MonoBehaviour {
    private EquipmentManager equipment;
    private CombatSkills mySkills;
    private UnitAnimController anim;
    private UnitStateMachine stateMachine;
    private AttackReactionSkills attackReaction;

    public float totalBlood;
    public bool inShock = false;
    public bool suffocating = false;
    public bool unconscious = false;

    private List<BodyPart> bodyParts = new List<BodyPart>();
    [SerializeField]
    private List<BodyPart> vitalParts;

    //for attack controller
    public List<BodyPart> attack1Parts;
    public List<BodyPart> attack2Parts;

    private void Start() //make private?
    {
        //unitReactions = GetComponent<UnitReactions>();
        stateMachine = GetComponent<UnitStateMachine>();
        anim = GetComponent<UnitAnimController>();
        mySkills = GetComponent<CombatSkills>();
        mySkills.onSkillGained += UpdateSkills;
        AddBodyParts();

        //if a unit doesn't have an equipment manager load default eqpmnt 
        EquipmentManager e = GetComponent<EquipmentManager>();
        if (e == null)
        {
            GetComponent<DefaultEquipment>().EquipLoadout(this);
        }
        else
        {
            //let equipment handler load it's own default stuff if it needs to
            e.onEquipmentChanged += UpdateSkills;
        }
    }

    private void CalculateTotalBlood()
    {
        int totalSeverity = 0;
        for(int i = 0; i < bodyParts.Count; i++)
        {
            totalSeverity += bodyParts[i].SeverityLevel();
        }
        totalBlood = (bodyParts.Count * 100f) - (25f * totalSeverity);
    }

    #region Public Methods
    //gives us a precentage to multiply skills by to get their current effectiveness
    public float OverallHealth()
    {
        float currentDamage = 0;

        for(int i = 0; i < bodyParts.Count; i++)
        {
            currentDamage += bodyParts[i].SeverityLevel();
        }

        float fullHealth = ((5 * bodyParts.Count) * bodyParts.Count);
        float health = (fullHealth - currentDamage) / fullHealth;
        return health;
    }

    public bool PartTooInjured(List<BodyPart> partsToCheck)
    {
        for (int i = 0; i < partsToCheck.Count; i++)
        {
            BodyPart p = partsToCheck[i];
            if (p.IsTooInjured())
            {
                return true;
            }
        }
        return false;
    }

    //for each BodyPart to load itself to the controller
    public void AddBodyParts()
    {
        bodyParts = GetComponent<Body>().GetBodyParts();

        LoadAttackParts(); //saves attack parts into a list for the AttackController to reference
        CalculateTotalBlood(); //bodyparts needed for this 
        UpdateSkills(); //relies on bodyparts being loaded

        LoadVitalParts();
    }

    public bool Incapacitated()
    {
        if (inShock || suffocating || unconscious)
            return true;
        else
            return false;    
    }

#endregion

    #region Recieve Attack

    public virtual void RecieveAttack(AttackInfo recievedAttack, Transform myAttacker)
    {
        //don't keep reacting to the same attack
        if (stateMachine.currentState != StateMachine.States.Fight  || 
            stateMachine.currentState != StateMachine.States.Flight || 
            stateMachine.currentState != StateMachine.States.FightOrFlight)
        {
            stateMachine.currentThreat = myAttacker;
            stateMachine.RequestChangeState(StateMachine.States.FightOrFlight);
        }
            
        if (Hit())
        {
            BodyPart bodyPart = GetRandomPart();
            bodyPart.TakeDamage(recievedAttack);
        }

        if (PartTooInjured(vitalParts))
        {
            stateMachine.RequestChangeState(StateMachine.States.Dead);
            string line = "<color=red>" + gameObject.name + " has been mortally wounded!</color>";
            BattleReport.AddToBattleReport(line);
        }
    }

    private bool Hit()
    {
        if (Incapacitated())
            return true;

        if (Random.Range(0, 100) <= attackReaction.dodge)
        {
            string line = "<color=blue>" + gameObject.name + " dodged the attack!</color>";
            FloatingTextController.CreateFloatingText("Dodge", transform, Color.blue);
            anim.Dodge();
            mySkills.ExperienceGain(CombatSkills.CombatSkill.Dodge, 50f);
            ReportFilter.AddToFilterList(gameObject.name);
            BattleReport.AddToBattleReport(line);
            return false;
        }
        else if (Random.Range(0, 100) <= attackReaction.block)
        {
            string line = "<color=blue>" + gameObject.name + " blocked the attack!</color>";
            FloatingTextController.CreateFloatingText("Block", transform, Color.blue);
            anim.Block();
            mySkills.ExperienceGain(CombatSkills.CombatSkill.Block, 50f);
            ReportFilter.AddToFilterList(gameObject.name);
            BattleReport.AddToBattleReport(line);
            return false;
        }
        else if (Random.Range(0, 100) <= attackReaction.parry)
        {
            string line = "<color=blue>" + gameObject.name + " parried the attack!</color>";
            FloatingTextController.CreateFloatingText("Parry", transform, Color.blue);
            anim.Parry();
            mySkills.ExperienceGain(CombatSkills.CombatSkill.Parry, 50f);
            GetComponent<AttackTimer>().ResetAttackTimer(0f); //allow unit to retaliate immediately
            ReportFilter.AddToFilterList(gameObject.name);
            BattleReport.AddToBattleReport(line);
            return false;
        }

        return true;
    }

    //picks a random part out of bodyParts
    private BodyPart GetRandomPart()
    {
        while (true)
        {
            int infiniteLoopStopper = 0;
            int n = Random.Range(0, bodyParts.Count);

            if (n > 0)
                n--;

            //prevents severed/obliterated part from being attacked
            if (bodyParts[n].SeverityLevel() < 5)
            {
                return bodyParts[n];
            }
            else if (infiniteLoopStopper >= 100)
            {
                Debug.LogError("Can't find bodypart after 100 tries");
                break;
            }
            infiniteLoopStopper++;
        }
        Debug.LogError("shouldn't be touching this");
        return bodyParts[0];
    }
    #endregion

    #region Equipping BodyParts
    public void EquipArmor(Item item)
    {
        for (int i = 0; i < bodyParts.Count; i++)
        {
            if (bodyParts[i].armorSlot == item.myEquipSlot)
            {
                bodyParts[i].EquipArmor(item);
            }
        }
    }

    public void EquipWeapon(Item item)
    {
        for (int i = 0; i < bodyParts.Count; i++)
        {
            if(bodyParts[i].weaponSlot == item.myEquipSlot)
            {
                bodyParts[i].EquipWeapon(item);
            }
        }
    }

    public void StripEquipment(Item.EquipmentSlot slot)
    {
        for (int i = 0; i < bodyParts.Count; i++)
        {
            if (bodyParts[i].weaponSlot == slot)
            {
                bodyParts[i].StripEquipment("Weapon");
            }
            else if (bodyParts[i].armorSlot == slot)
            {
                bodyParts[i].StripEquipment("Armor");
            }
        }
    }

    //returns a list of parts with attack1
    private void LoadAttackParts()
    {
        Attack1Parts();
        Attack2Parts();
    }

    //returns a list of parts with attack1
    private void Attack1Parts()
    {
        for (int i = 0; i < bodyParts.Count; i++)
        {
            if (bodyParts[i].attack1)
            {
                attack1Parts.Add(bodyParts[i]);
                //Debug.Log("added " + bodyParts[i] + " for " + gameObject.name);
            }
        }
    }

    //returns a list of pasts with attack2
    private void Attack2Parts()
    {
        for (int i = 0; i < bodyParts.Count; i++)
        {
            if (bodyParts[i].attack2)
            {
                attack2Parts.Add(bodyParts[i]);
                //Debug.Log("added " + bodyParts[i] + " for " + gameObject.name);
            }
        }
    }

    //returns a list of pasts with attack2
    private void LoadVitalParts()
    {
        for (int i = 0; i < bodyParts.Count; i++)
        {
            if (bodyParts[i].isVitalPart)
            {
                vitalParts.Add(bodyParts[i]);
                //Debug.Log("added " + bodyParts[i].name + " for " + gameObject.name + " as vital part");
            }
        }
    }

    #endregion

    #region Bleeding

    public void Bleed(float amount)
    {
        StartCoroutine(Bleeding(amount));
    }

    private IEnumerator Bleeding(float blood)
    {
        //using 20 here because 12 parts * 5 severity = 1200 (roughly, this gets inaacurate with creatures with more/less parts)
        float bloodNeeded = 600; //half of 1200 which I've set as a hardcoded default blood volume for all units

        while (blood > 0 && totalBlood > 0)
        {
            totalBlood -= blood;
            blood = (blood / 2) - 1f;
            //Debug.Log(gameObject.name + " is bleeding for " + damage + " damage");
            yield return new WaitForSeconds(1f);
        }

        if (!inShock && totalBlood <= bloodNeeded)
        {
            SlipIntoShock();
            inShock = true;
        }

        yield break;
    }

    private void SlipIntoShock()
    {
        string line = "<color=red>" + gameObject.name + " is experiencing shock from loss of blood!</color>";
        BattleReport.AddToBattleReport(line);
        stateMachine.TriggerTemporaryState(IncapacitatedState.TemporaryState.InShock, 30f);
    }
    #endregion

    #region Saving and Loading BodyParts

    //applies saved info to bodyparts
    public void LoadSavedParts(BodyPart.PartInfo[] info)
    {
        if (bodyParts.Count == 0)
            AddBodyParts();

        for (int i = 0; i < info.Length; i++)
        {
            for (int b = 0; b < bodyParts.Count; b++)
            {
                if (bodyParts[b].name == info[i].name)
                {
                    bodyParts[b].UnpackSavedPartInfo(info[i]);
                }

            }
        }
        CalculateTotalBlood();
    }

    public BodyPart.PartInfo[] GetBodyParts()
    {
        BodyPart.PartInfo[] info = new BodyPart.PartInfo[bodyParts.Count];

        for(int i = 0; i < bodyParts.Count; i++)
        {
            info[i] = bodyParts[i].PackagePartInfo();
        }

        return info;
    }

    #endregion

    private void UpdateSkills()
    {
        if (mySkills == null) //need to check for this in case this is called while loading saved parts before default parts are loaded
            mySkills = GetComponent<CombatSkills>();
        attackReaction = mySkills.GetAttackReactionSkills();
    }
}
