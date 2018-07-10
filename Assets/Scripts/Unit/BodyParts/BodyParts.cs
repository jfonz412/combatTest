﻿using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Linq;

public class BodyParts : MonoBehaviour {
    public enum BodyType { Human, BushBanshee, FourLeggedAnimal }
    public enum Parts { Head, Neck, LeftArm, RightArm, LeftHand, RightHand, Chest, Abdomin, LeftLeg, RightLeg, LeftFoot, RightFoot }

    public BodyType bodyType;

    protected Dictionary<Parts, int> bodyPartDamage; //the int is the severityLevel of the damage
    protected Parts[] vitalParts;

    protected Dictionary<EquipmentSlot, ArmorInfo> myArmor = new Dictionary<EquipmentSlot, ArmorInfo>();

    protected EquipmentManager equipment;
    protected CombatSkills mySkills;
    protected UnitReactions unitReactions;
    protected UnitStatusController statusController;
    protected UnitAnimController anim;
    protected AttackReactionSkills attackReaction;
    protected Brain myBrain;

    public float totalBlood = 1200; //hardcoded, come up with a better formula for this?

    public delegate void OnDamageTaken(DamageInfo info);
    public OnDamageTaken onDamageTaken;
    public delegate void OnHealthLoaded();
    public OnHealthLoaded onHealthLoaded;

    protected void Awake()
    {
        equipment = GetComponent<EquipmentManager>();
        equipment.onEquipmentChanged += GetArmor;
    }

    protected virtual void Start()
    {
        mySkills = GetComponent<CombatSkills>();
        unitReactions = GetComponent<UnitReactions>();
        anim = GetComponent<UnitAnimController>();
        statusController = GetComponent<UnitStatusController>();
        mySkills.onSkillGained += UpdateSkills;
        myBrain = GetComponent<Brain>();

        UpdateSkills(); 
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log(gameObject.name + "'s blood: " + totalBlood);
        }
    }

    public bool VitalPartInjured(Parts[] partsToCheck)
    {
        for (int i = 0; i < partsToCheck.Length; i++)
        {
            Parts p = partsToCheck[i];
            if (bodyPartDamage[p] >= BodyPartDamageLimits.partDamageLimits[p])
            {
                return true;
            }
        }
        return false;
    }

    public virtual void RecieveAttack(AttackInfo recievedAttack, Transform myAttacker)
    {
        unitReactions.ReactToAttackAgainstSelf(myAttacker);
        DetermineImpact(recievedAttack);
    }

    protected void DetermineImpact(AttackInfo recievedAttack)
    {
        string attacker = recievedAttack.attackerName;
        string weapon = recievedAttack.weapon.name;

        if (Hit())
        {
            DamageInfo damageRecieved = CalculateDamage(recievedAttack);
            float defenseThreshold = 5f;

            if (damageRecieved.damageDealt > defenseThreshold)
            {
                Damage(damageRecieved);
            }
            else
            {
                string line = attacker + " did no damage to " + gameObject.name + ".";

                FloatingTextController.CreateFloatingText("No Damage", transform, Color.white);
                BattleReport.AddToBattleReport(line);
            }
        }
    }

    //these thresholds should stay the same, a bronze colluses would not as vulnerable as a human
    //but this would be reflected in the armor stats (bronze colluses's bare "flesh" will have a hardness rating of iron armor)
    //damage is attack - defense 
    protected void Damage(DamageInfo damageInfo)
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
        Injuries.DamageMessage(damageInfo);
        DamageBodyPart(damageInfo);

        if (VitalPartInjured(vitalParts))
        {
            myBrain.Die();
            string line = "<color=red>" + gameObject.name + " has been mortally wounded!</color>";
            BattleReport.AddToBattleReport(line);
        }
        else
        {
            statusController.CheckForStatusTriggers(damageInfo);
        }
    }

    protected void DamageBodyPart(DamageInfo info)
    {
        Parts p = info.bodyPart;
        int d = info.severityLevel;

        if(d > bodyPartDamage[p])
            bodyPartDamage[p] = d;

        if (onDamageTaken != null)
            onDamageTaken.Invoke(info);
    }

    protected bool Hit()
    {
        if (Random.Range(0, 100) <= attackReaction.dodge)
        {
            string line = "<color=green>" + gameObject.name + " parried the attack!</color>";
            FloatingTextController.CreateFloatingText("Parry", transform, Color.green);
            anim.Dodge();
            mySkills.ExperienceGain(CombatSkills.CombatSkill.Dodge, 50f);
            BattleReport.AddToBattleReport(line);
            return false;
        }
        else if (Random.Range(0, 100) <= attackReaction.block)
        {
            string line = "<color=blue>" + gameObject.name + " blocked the attack!</color>";
            FloatingTextController.CreateFloatingText("Block", transform, Color.blue);
            anim.Block();
            mySkills.ExperienceGain(CombatSkills.CombatSkill.Block, 50f);
            BattleReport.AddToBattleReport(line);
            return false;
        }
        else if (Random.Range(0, 100) <= attackReaction.parry)
        {
            string line = "<color=yellow>" + gameObject.name + " parried the attack!</color>";
            FloatingTextController.CreateFloatingText("Parry", transform, Color.yellow);
            anim.Parry();
            mySkills.ExperienceGain(CombatSkills.CombatSkill.Parry, 50f);
            GetComponent<AttackTimer>().ResetAttackTimer(0f);
            BattleReport.AddToBattleReport(line);
            return false;
        }

        return true;
    }

    protected DamageInfo CalculateDamage(AttackInfo recievedAttack)
    {
        DamageInfo damageInfo = new DamageInfo();

        Parts bodyPart = GetRandomPart();
        ArmorInfo armor = GetArmorFrom(bodyPart.ToString());
        float weaponHardness = recievedAttack.weapon.hardnessValue;
        float enemyAttack = weaponHardness + recievedAttack.force;

        if (recievedAttack.weapon.sharpness > .60f)
        {
            damageInfo.damageType = Injuries.DamageType.Penetration;
        }
        else
        {
            damageInfo.damageType = Injuries.DamageType.Impact;
        }

        //crit hit
        if (Random.Range(0, 100) <= recievedAttack.skill)
        {
            string line = "<color=magenta>" + recievedAttack.attackerName + " skillfully lands a critical hit with their " + recievedAttack.weapon.name + "!</color>";
            Color color = new Color(0.2F, 0.3F, 0.4F);
            anim.TakeDamage(Color.magenta);
            FloatingTextController.CreateFloatingText("Vulnerability targeted!", transform, color);
            BattleReport.AddToBattleReport(line);
            damageInfo.damageDealt = enemyAttack;
        }
        else
        {
            float myDefense = armor.protectionValue;
            damageInfo.damageDealt = enemyAttack - myDefense;
        }

        damageInfo.bodyType = bodyType;
        damageInfo.bodyPart = bodyPart;
        damageInfo.armorName = armor.name;
        damageInfo.weaponName = recievedAttack.weapon.name;
        damageInfo.victimName = gameObject.name;
        damageInfo.attackerName = recievedAttack.attackerName;
        return damageInfo;
    }

    //checks for bodyPart in any of the equipment sections
    protected virtual ArmorInfo GetArmorFrom(string bodyPart)
    {
        Debug.Log("Should not be touching this base method!!!");
        return new ArmorInfo();
    }

    //picks a random part out of bodyPartDamage
    protected Parts GetRandomPart()
    {
        int n = Random.Range(0, bodyPartDamage.Count);

        if (n > 0)
            n--;

        return bodyPartDamage.Keys.ElementAt(n);
    }

    //gives us a precentage to multiply skills by to get their current effectiveness
    public virtual float OverallHealth()
    {
        float fullHealth = ((5 * bodyPartDamage.Count) * bodyPartDamage.Count);
        float currentDamage = (bodyPartDamage.Sum(x => x.Value) * bodyPartDamage.Count);
        float health = (fullHealth - currentDamage) / fullHealth;
        return health;
    }

    protected virtual void GetArmor(Equipment oldItem, Equipment newItem)
    {
        Debug.LogError("Base method should not be called here!");
    }

    //put together here and given to injuries script
    public struct DamageInfo
    {
        public string armorName;
        public string weaponName;
        public string attackerName;
        public string victimName;
        public float damageDealt;
        public int severityLevel;
        public Injuries.DamageType damageType;
        public Parts bodyPart;
        public BodyType bodyType;
    }

    public void LoadPartDamage(Dictionary<Parts, int> savedbodyPartDamage)
    {
        bodyPartDamage = savedbodyPartDamage;
        totalBlood = 1200 - (bodyPartDamage.Sum(x => x.Value) * 20);

        if (onHealthLoaded != null)
            onHealthLoaded.Invoke();
    }

    public Dictionary<Parts, int> GetPartDamage()
    {
        return bodyPartDamage;
    }

    protected void UpdateSkills()
    {
        attackReaction = mySkills.GetAttackReactionSkills();
    }
}
