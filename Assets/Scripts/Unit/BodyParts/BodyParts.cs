using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Linq;

public class BodyParts : MonoBehaviour {
    public enum BodyType { Human, BushBanshee, FourLeggedAnimal }
    public enum Parts { Head, Neck, LeftArm, RightArm, LeftHand, RightHand, Chest, Abdomin, LeftLeg, RightLeg, LeftFoot, RightFoot }

    public BodyType bodyType;

    protected Dictionary<Parts, float> bodyPartHealth;
    protected Parts[] vitalParts;

    protected Dictionary<EquipmentSlot, ArmorInfo> myArmor = new Dictionary<EquipmentSlot, ArmorInfo>();

    protected EquipmentManager equipment;
    protected CombatSkills mySkills;
    protected UnitReactions unitReactions;
    protected UnitStatusController statusController;
    protected Death deathController;
    protected UnitAnimController anim;
    protected AttackReactionSkills attackReaction;

    protected float totalBlood; //BloodPerBodyPart * number of bodyparts
    private bool alive = true;

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
        deathController = GetComponent<Death>();
        anim = GetComponent<UnitAnimController>();
        statusController = GetComponent<UnitStatusController>();
        mySkills.onSkillGained += UpdateSkills;

        UpdateSkills(); 
    }

    public bool VitalPartInjured(Parts[] partsToCheck)
    {
        for (int i = 0; i < partsToCheck.Length; i++)
        {
            if (bodyPartHealth[partsToCheck[i]] <= 0)
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
        int severityID;
        Color color = Color.red;

        if (damage <= 50)
        {
            color.r = .20f;
            severityID = 0;
        }
        else if (damage <= 70)
        {
            color.r = .40f;
            severityID = 1;
        }
        else if (damage <= 90)
        {
            color.r = .60f;
            severityID = 2;
        }
        else if (damage <= 100)
        {
            color.r = .80f;
            severityID = 3;
        }
        else if (damage <= 120)
        {
            color.r = .90f;
            severityID = 4;
        }
        else
        {
            color.r = 100f;
            severityID = 5;
        }

        damageInfo.severityID = severityID;
        FloatingTextController.CreateFloatingText("Hit", transform, color);
        anim.TakeDamage(color);
        Injuries.DamageMessage(damageInfo);
        DamageBodyPart(damageInfo);

        statusController.CheckForStatusTrigger(damageInfo);

        if (!VitalPartInjured(vitalParts))
        {
            StartCoroutine(Bleeding(damage));
        }
        else
        {
            TriggerDeath(); //this could be triggered while bleeding out wich will also trigger death resulting in multiple orbs
        }
    }

    protected void DamageBodyPart(DamageInfo info)
    {
        Parts p = info.bodyPart;
        float d = info.damageDealt;

        bodyPartHealth[p] -= d;

        if (bodyPartHealth[p] < 0f)
            bodyPartHealth[p] = 0f;

        if (onDamageTaken != null)
            onDamageTaken.Invoke(info);

        //will be null for everyone except player
        //Debug.Log(bodyPart + "health is " + bodyPartHealth[bodyPart] + " for " + gameObject.name);
    }

    protected void CheckForStatusChange()
    {
        //stunned
        //knocked out
        //panic
        //beserk
        //knocked off balance?
    }

    protected IEnumerator Bleeding(float damage)
    {
        while (damage > 0)
        {
            totalBlood -= damage;
            //Debug.Log(gameObject.name + " just bled " + damage + " damage. Blood remaining: " + totalBlood);
            damage = (damage / 2) - 1f;
            yield return new WaitForSeconds(1f);
        }

        if (totalBlood <= 0 ) 
        {
            TriggerDeath();
        }
        yield break;
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

        CheckForStatusChange(); //empty right now
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

    //picks a random part out of bodyPartHealth
    protected Parts GetRandomPart()
    {
        int n = Random.Range(0, bodyPartHealth.Count);

        if (n > 0)
            n--;

        return bodyPartHealth.Keys.ElementAt(n);
    }

    public virtual float OverallHealth()
    {
        //gives us a precentage to multiply skills by to get their current effectiveness
        return (bodyPartHealth.Sum(x => x.Value) / bodyPartHealth.Count) * 0.01f;
    }

    protected virtual void GetArmor(Equipment oldItem, Equipment newItem)
    {
        Debug.LogError("Base method should not be called here!");
    }

    protected void TriggerDeath()
    {
        //this method alerts others, and since this unit is dead UnitReactionManager should skip over it
        //unitReactions.ReactToAttackAgainstSelf(myAttacker); //might not neec this because unit is alerted with each hit even if 1hko
        StopAllCoroutines(); 
        unitReactions.isDead = true; //stop reacting
        deathController.Die();
    }

    //put together here and given to injuries script
    public struct DamageInfo
    {
        public string armorName;
        public string weaponName;
        public string attackerName;
        public string victimName;
        public float damageDealt;
        public int severityID;
        public Injuries.DamageType damageType;
        public Parts bodyPart;
        public BodyType bodyType;
    }

    public void LoadBodyPartHealth(Dictionary<Parts, float> savedBodyPartHealth)
    {
        bodyPartHealth = savedBodyPartHealth;
        totalBlood = bodyPartHealth.Sum(x => x.Value);

        if (onHealthLoaded != null)
            onHealthLoaded.Invoke();
    }

    public Dictionary<Parts, float> GetBodyPartHealth()
    {
        return bodyPartHealth;
    }

    protected void UpdateSkills()
    {
        attackReaction = mySkills.GetAttackReactionSkills();
    }
}
