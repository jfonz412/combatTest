using System;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : MonoBehaviour {
    public DollPart dollPart; //for player only for now
    private List<string> injuryLog = new List<string>(); //must be instantiated here

    public new string name;

    private Brain myBrain;
    private BodyPartController myBody;
    private CombatSkills myCombatSkills;
    private UnitAnimController anim;

    public string[] stabInjuries = new string[6];
    public string[] hackInjuries = new string[6];
    public string[] biteInjuries = new string[6];
    public string[] bluntInjuries = new string[6];
    public string[] clawInjuries = new string[6];
    public string[] projectileInjuries = new string[6];



    //used to determine if this bodypart is needed to attack
    public bool attack1 = false;
    public bool attack2 = false;

    public Item.EquipmentSlot weaponSlot = Item.EquipmentSlot.NA;
    public Item.EquipmentSlot armorSlot = Item.EquipmentSlot.NA;
    [SerializeField]
    private Item myWeapon, myArmor; 


    public delegate void OnEquipmentChanged(Item oldItem, Item newItem);
    public OnEquipmentChanged onEquipmentChanged;

    private int currentSeverityLevel = 0;

    //severity levels will never reach 6, so by default none of these will trigger
    public int knockoutThreshold = 6;
    public int vomitThreshold = 6;
    public int rockedThreshold = 6;
    public int downedThreshold = 6;
    public int cantBreathThreshold = 6;
    public int suffocationThreshold = 6;
    public int functioningLimit = 6;

    //if true and this part is damaged beyond it's functioningLimit, unit will be mortally wounded
    public bool isVitalPart = false;

    public float naturalDefense = 0; //defense when not wearing any armor, may be replaced by default armor item
    public float bleedBonus = 0f; //how rapidly we bleed from this part

    private void Start()
    {
        myBrain = GetComponent<Brain>();
        myBody = GetComponent<BodyPartController>();
        myCombatSkills = GetComponent<CombatSkills>();
        anim = GetComponent<UnitAnimController>();

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

    public float MyArmor() 
    {
        if (myArmor == null)
        {
            return naturalDefense;
        }
        else
        {
            return myArmor.hardnessValue + naturalDefense;
        }
    }

    public void EquipWeapon(Item newWeapon)
    {
        if (newWeapon != null) { }
        myWeapon = newWeapon;
    }

    public void EquipArmor(Item newArmor)
    {
        if (newArmor != null)
            myArmor = newArmor;
    }

    public void StripEquipment(string type)
    {
        if (type == "Armor")
        {
            myArmor = null;
        }
        else if (type == "Weapon")
        {
            myWeapon = null;
        }
        else
        {
            Debug.LogError("String not found!");
        }
    }

    //if this bodypart is checked for a weapon and doesn't have one it needs to tell AttackInfo something about what it's attacking with
    private Item Unarmed()
    {
        Debug.LogWarning("Equipping hardcoded fist");
        return MasterItemList.Fist();
    }
    #endregion

#region Damage Calculations

    public void TakeDamage(AttackInfo recievedAttack)
    {
        DamageInfo damageInfo = DetermineSeverityLevel(recievedAttack);
        int severity = damageInfo.severityLevel;
        string line;

        if (severity >= 0)
        {
            if (severity > currentSeverityLevel)
            {
                currentSeverityLevel = severity;
                if (dollPart != null)
                    dollPart.SeverityColor(severity);
            }
            string log;
            if (dollPart != null)
            {
                //Debug.Log(gameObject.name + " is logging for " + damageInfo.damageType + " damage!");
                log = dollPart.LogInjury(severity, damageInfo.damageType);
            }
            string unitName = gameObject.name;
            line = string.Format(GetInjuryString(damageInfo.damageType, severity), unitName, damageInfo.weaponName, damageInfo.attackerName);
            BattleReport.AddToBattleReport(line);
            StatusChecks(severity);
            Bleed(damageInfo);
        }
        else
        {
            line = recievedAttack.attackerName + " 's " + recievedAttack.weapon.name + " does no damage to " + gameObject.name + "'s " + name;
            BattleReport.AddToBattleReport(line);
        }
    }
        

    private DamageInfo DetermineSeverityLevel(AttackInfo receivedAttack)
    {
        DamageInfo damageInfo = CalculateDamage(receivedAttack);

        float damage = damageInfo.damageDealt;
        int severityLevel;
        Color color = Color.red;

        if (damage <= 20)
        {
            severityLevel = -1;
        }
        else if (damage <= 50)
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
        if(severityLevel >= 0)
        {
            FloatingTextController.CreateFloatingText("Hit", transform, color);
            anim.TakeDamage(color);
        }
        return damageInfo;
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
            float myDefense = MyArmor();
            damageInfo.damageDealt = enemyAttack - myDefense;
        }

        return damageInfo;
    }

    #endregion

#region methods

    private void StatusChecks(int severity)
    {
        //check for status effects in each part
    }

    private void SetInjuryStrings()
    {

    }
    private void AssignPartStats()
    {
        //assigns all thresholds, stats, ect
    }

    private void Bleed(DamageInfo info)
    {
        float bloodLoss = ((bleedBonus + info.severityLevel) * info.damageDealt) / 2f;
        Debug.Log("Bloodloss for " + gameObject.name + "'s " + name + " is " + bloodLoss);

        if(bloodLoss > 0)
            myBody.Bleed(bloodLoss);
    }

    private string GetInjuryString(Item.AttackType attackType, int severity)
    {
        if(attackType == Item.AttackType.Stab)
        {
            return stabInjuries[severity];
        }
        else if (attackType == Item.AttackType.BluntImpact)
        {
            return bluntInjuries[severity];
        }
        else if (attackType == Item.AttackType.Hack)
        {
            return hackInjuries[severity];
        }
        else
        {
            Debug.LogError("Attack type not found!");
            return "ATTACK TYPE NOT FOUND!";
        }
    }

    #endregion

#region Status Checks
    private void KnockoutCheck(int severity)
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

    private void RockedCheck(int severity)
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

    private void DownedCheck(int severity)
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

    private void CantBreathCheck(int severity)
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

    private void VomitCheck(int severity)
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
        //Debug.Log(gameObject.name + " unpacking " + info.name + " in " + name);
        currentSeverityLevel = info.severityLevel;
        //Debug.Log("Loading saved severity level for " + name + " at " + info.severityLevel);
        injuryLog = info.injuryLog;
        EquipArmor(info.myArmor);
        EquipWeapon(info.myWeapon);

        if (dollPart != null)
        {
            dollPart.Initialize(info);
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
