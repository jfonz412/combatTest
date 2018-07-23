using System;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : MonoBehaviour {
    public DollPart dollPart; //for player only for now
    
    public new string name;
    public List<string> injuryLog = new List<string>(); //must be instantiated here

    protected Brain myBrain;
    protected BodyPartController myBody;
    protected CombatSkills myCombatSkills;
    protected UnitAnimController anim;
    protected EquipmentManager myEquipment;

    protected Dictionary<Item.AttackType, string[]> myInjuryStrings;

    public bool attack1, attack2; //select these so that our attack controller can grab a refrence to them
    protected Item myWeapon; // OR UNARMED BASE ATTACK

    protected Item.EquipmentSlot armorType; //can be null or NA if the unit doesn't wear armor and will handle it's own defense 
    protected Item myArmor;

    public delegate void OnEquipmentChanged(Item oldItem, Item newItem);
    public OnEquipmentChanged onEquipmentChanged;

    protected int currentSeverityLevel = 0;

    //these will be set in the Start() function of each individual part 
    protected int knockoutThreshold;
    protected int vomitThreshold;
    protected int rockedThreshold;
    protected int downedThreshold;
    protected int cantBreathThreshold;
    protected int suffocationThreshold;
    protected int functioningLimit;

    //for setting individual thresholds for each bodypart..
    /*
    protected int severityThresh0;
    protected int severityThresh1;
    protected int severityThresh2;
    protected int severityThresh3;
    protected int severityThresh4;
    protected int severityThresh5;
    */

    protected float naturalDefense = 0;
    protected float bleedRate = 1f; //how rapidly we bleed from this part

    protected void Start()
    {
        myBrain = GetComponent<Brain>();
        myBody = GetComponent<BodyPartController>();
        myCombatSkills = GetComponent<CombatSkills>();
        anim = GetComponent<UnitAnimController>();
        myBody.AddBodyPart(this);


        AssignPartStats();
        SetInjuryStrings();

        if (dollPart != null)
        {
            dollPart.Initialize(PackagePartInfo());
        }

        //Debug.Log("assigning parts for " + this);
        //Debug.Log("my armortype is " + armorType);
    }

#region My Weapon and Armor
    public Item MyWeapon()
    {
        if(myWeapon == null)
        {
            return Unarmed();
        }
        else
        {
            return myWeapon;
        }
    }

    public float MyArmorValue() //might not need to be public
    {
        if (myArmor == null)
        {
            return naturalDefense;
        }
        else
        {
            return myArmor.hardnessValue;
        }
    }

    protected virtual Item Unarmed()
    {
        Debug.LogError("Should not touch base method");
        return new Item();
    }

    protected virtual Item Unarmored()
    {
        Debug.LogError("Should not touch base method");
        return new Item();
    }

    //because chest covers multiple parts, it should automatically place it's refrence in each applicable part
    public void EquipAsArmor(Item item)
    {
        if (item == null)
            return;

        if (armorType != Item.EquipmentSlot.NA) //if this part can equip armor
        {
            if (armorType == item.myEquipSlot)
            {
                if (onEquipmentChanged != null)
                {
                    onEquipmentChanged.Invoke(myArmor, item);
                }
            }
        }
    }

    public void EquipAsWeapon(Item item)
    {
        if (item == null)
            return;

        //check if we can equip w/ current skills?
        if (attack1 || attack2) //if this part is responsible for attacking
        {
            myWeapon = item;

            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(myWeapon, item);
            }
        }
    }

    #endregion

#region Damage Calculations

    public void TakeDamage(AttackInfo recievedAttack)
    {
        DamageInfo damageInfo = CalculateDamage(recievedAttack);
        damageInfo = DetermineSeverityLevel(damageInfo);
        int severity = damageInfo.severityLevel;
        string line = string.Format(GetInjuryString(damageInfo.damageType, severity), gameObject.name, damageInfo.weaponName);

        if (severity > currentSeverityLevel)
        {
            currentSeverityLevel = severity;
            if (dollPart != null)
                dollPart.SeverityColor(severity);
        }

        string log;
        if (dollPart != null)
        {
            Debug.Log(gameObject.name + " is trying to log for " + damageInfo.damageType + " damage!");
            log = dollPart.LogInjury(severity, damageInfo.damageType);
        }
            

        //injuryLog.Add(log); these should be connected
        BattleReport.AddToBattleReport(line);

        StatusChecks(severity);
        Bleed(damageInfo);
    }

     private DamageInfo CalculateDamage(AttackInfo recievedAttack)
    {
        DamageInfo damageInfo = new DamageInfo();

        damageInfo.damageType = recievedAttack.weapon.myAttackType;
        damageInfo.weaponName = recievedAttack.weapon.name;
        damageInfo.victimName = gameObject.name;
        damageInfo.attackerName = recievedAttack.attackerName;

        float enemyAttack = recievedAttack.weapon.hardnessValue + recievedAttack.force;

        //crit hit
        if (UnityEngine.Random.Range(0, 100) <= recievedAttack.skill)
        {
            damageInfo.damageDealt = enemyAttack; //no armor or def taken into account
        }
        else
        {
            float myDefense = MyArmorValue();
            damageInfo.damageDealt = enemyAttack - myDefense;
        }

        return damageInfo;
    }

    private DamageInfo DetermineSeverityLevel(DamageInfo damageInfo)
    {              
        float damage = damageInfo.damageDealt;
        int severityLevel;
        Color color = Color.red;

        if (damage <= 50)
        {
            color.r = .20f;
            severityLevel = 0;
        }
        else if (damage <= 70)
        {
            color.r = .40f;
            severityLevel = 1;
        }
        else if (damage <= 90)
        {
            color.r = .60f;
            severityLevel = 2;
        }
        else if (damage <= 100)
        {
            color.r = .80f;
            severityLevel = 3;
        }
        else if (damage <= 120)
        {
            color.r = .90f;
            severityLevel = 4;
        }
        else
        {
            color.r = 100f;
            severityLevel = 5;
        }

        damageInfo.severityLevel = severityLevel;
        FloatingTextController.CreateFloatingText("Hit", transform, color);
        anim.TakeDamage(color);
        return damageInfo;
    }
#endregion

#region Virtual methods

    protected virtual void StatusChecks(int severity)
    {
        //check for status effects in each part
    }

    protected virtual void SetInjuryStrings()
    {

    }
    protected virtual void AssignPartStats()
    {
        //assigns all thresholds, stats, ect
    }

    protected virtual void Bleed(DamageInfo info)
    {
        float bloodLoss = (bleedRate + info.severityLevel) * info.damageDealt;
        myBody.Bleed(bloodLoss);
    }

    protected string GetInjuryString(Item.AttackType attackType, int severity)
    {
        return myInjuryStrings[attackType][severity];
    }

    #endregion

#region Status Checks
    protected void KnockoutCheck(int severity)
    {
        int multiplier = 10; // * severityLevel to get time knocked out

        if (severity >= knockoutThreshold)
        {
            if (UnityEngine.Random.Range(0, 100) > myCombatSkills.statusResistance / (severity + 1))
            {
                int duration = severity * multiplier;

                myBrain.TriggerTemporaryState(Brain.State.Unconscious, duration);
                anim.KnockedOut();
                string line = "<color=blue>" + gameObject.name + " has been knocked unconscious by the attack!</color>";
                BattleReport.AddToBattleReport(line);
            }
        }
    }

    protected void RockedCheck(int severity)
    {
        if (severity >= rockedThreshold)
        {
            if (UnityEngine.Random.Range(0, 100) > myCombatSkills.statusResistance / (severity + 1))
            {
                myBrain.TriggerTemporaryState(Brain.State.Rocked, severity);
                anim.Rocked();
                string line = "<color=blue>" + gameObject.name + " was rocked by the attack!</color>";
                BattleReport.AddToBattleReport(line);
            }
        }
    }

    protected void DownedCheck(int severity)
    {
        if (severity >= downedThreshold)
        {
            if (UnityEngine.Random.Range(0, 100) > myCombatSkills.statusResistance / (severity + 1))
            {
                myBrain.TriggerTemporaryState(Brain.State.Downed, severity);
                anim.FallOver();
                string line = "<color=blue>" + gameObject.name + " is knocked to the ground!</color>";
                BattleReport.AddToBattleReport(line);
            }
        }
    }

    protected void CantBreathCheck(int severity)
    {
        if (severity >= cantBreathThreshold)
        {
            if (severity >= suffocationThreshold) //if the injury was bad enough
            {
                myBody.Suffocate(this); //tell bodyPartController and return
                return;
            }

            if (UnityEngine.Random.Range(0, 100) > myCombatSkills.statusResistance / (severity + 1))
            {
                myBrain.TriggerTemporaryState(Brain.State.CantBreathe, severity);
                anim.CantBreath();
                string line = "<color=blue>" + gameObject.name + " is struggling to breathe!</color>";
                BattleReport.AddToBattleReport(line);
            }
        }
    }

    protected void VomitCheck(int severity)
    {
        if (severity >= vomitThreshold)
        {
            if (UnityEngine.Random.Range(0, 100) > myCombatSkills.statusResistance / (severity + 1))
            {
                myBrain.TriggerTemporaryState(Brain.State.Vomitting, severity);
                anim.Vomit(); //ANIM MUST COME AFTER STATE IS FLIPPED
                string line = "<color=blue>The injury causes " + gameObject.name + " to vomit!</color>";
                BattleReport.AddToBattleReport(line);
            }
        }
    }
    #endregion

    #region Public Methods
    public bool IsTooInjured()
    {
        if (currentSeverityLevel >= functioningLimit)
        {
            return true;
        }
        return false;
    }
    public int SeverityLevel()
    {
        return currentSeverityLevel;
    }

    public int SuffocationThreshold()
    {
        return cantBreathThreshold;
    }

    public struct DamageInfo
    {
        public string weaponName;
        public string attackerName;
        public string victimName;
        public float damageDealt;
        public int severityLevel;
        public string bodyPart;
        public Item.AttackType damageType;
    }

    public PartInfo PackagePartInfo()
    {
        PartInfo info = new PartInfo();
        info.severityLevel = currentSeverityLevel;
        info.myArmor = myArmor;
        info.myWeapon = myWeapon;
        info.injuryLog = injuryLog;
        info.name = name;
        return info;
    }

    public void UnpackSavedPartInfo(PartInfo info)
    {
        if(info.name == name)
        {
            currentSeverityLevel = info.severityLevel;
            injuryLog = info.injuryLog;
            EquipAsArmor(info.myArmor);
            EquipAsWeapon(info.myWeapon);

            if (dollPart != null)
            {
                dollPart.Initialize(info);
            }
        }
    }

    [Serializable]
    public struct PartInfo
    {
        public string name;
        public int severityLevel;
        public Item myArmor;
        public Item myWeapon;
        public List<string> injuryLog;
    }
    #endregion
}
