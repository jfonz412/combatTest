using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Linq;

public class BodyParts : MonoBehaviour {
    public enum Parts { Head, Neck, LeftArm, RightArm, LeftHand, RightHand, Chest, Abdomin, LeftLeg, RightLeg, LeftFoot, RightFoot }
    protected Dictionary<Parts, float> bodyPartHealth;
    protected Parts vitalPart; //if this is destroyed, unit is killed. Should make this an array eventually

    protected Dictionary<EquipmentSlot, ArmorInfo> myArmor = new Dictionary<EquipmentSlot, ArmorInfo>();

    protected EquipmentManager equipment;
    protected CombatSkills mySkills;
    protected UnitReactions unitReactions;
    protected Death deathController;

    protected AttackReactionSkills attackReaction;

    protected float totalBlood; //BloodPerBodyPart * number of bodyparts
    private bool alive = true;

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

        mySkills.onSkillGained += UpdateSkills;

        UpdateSkills(); 
    }

    public virtual void RecieveAttack(AttackInfo recievedAttack, Transform myAttacker)
    {
        unitReactions.ReactToAttackAgainstSelf(myAttacker);
        DetermineImpact(recievedAttack);
    }

    public bool CheckBodyParts(Parts[] partsToCheck)
    {
        for(int i = 0; i < partsToCheck.Length; i++)
        {
            if(bodyPartHealth[partsToCheck[i]] <= 0)
            {
                return false;
            }
        }
        return true;
    }

    protected void DetermineImpact(AttackInfo recievedAttack)
    {
        string attacker = recievedAttack.attackerName;
        string weapon = recievedAttack.weapon.name;

        if (Hit())
        {
            DamageInfo damageRecieved = CalculateDamage(recievedAttack);
            float defenseThreshold = 25;

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
        Color32 color;

        if (damage <= 50)
        {
            color = new Color32(55, 0, 0, 255);
            severityID = 0;
        }
        else if (damage <= 70)
        {
            color = new Color32(105, 0, 0, 255);
            severityID = 1;
        }
        else if (damage <= 90)
        {
            color = new Color32(155, 0, 0, 255);
            severityID = 2;
        }
        else if (damage <= 100)
        {
            color = new Color32(205, 0, 0, 255);
            severityID = 3;
        }
        else if (damage <= 120)
        {
            color = new Color32(255, 0, 0, 255);
            severityID = 4;
        }
        else
        {
            color = new Color32(255, 0, 0, 255);
            severityID = 5;
        }

        damageInfo.severityID = severityID;
        FloatingTextController.CreateFloatingText("Hit", transform, color);
        CreateDamageReport(damageInfo);
        DamageBodyPart((Parts)damageInfo.bodyPartID, damage);

        if (!Killed())
        {
            StartCoroutine(Bleeding(damage));
        }
        else
        {
            TriggerDeath();
        }
    }

    protected virtual void CreateDamageReport(DamageInfo damageInfo)
    {

    }

    protected void DamageBodyPart(Parts bodyPart, float damage)
    {
        bodyPartHealth[bodyPart] -= damage;

        if (bodyPartHealth[bodyPart] < 0f)
            bodyPartHealth[bodyPart] = 0f;

        Debug.Log(bodyPart + "health is " + bodyPartHealth[bodyPart] + " for " + gameObject.name);
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

        if (Killed()) //or if instantlyKilled
        {
            TriggerDeath();
            StopAllCoroutines();
        }
        yield break;
    }

    protected bool Killed()
    {
        if(totalBlood <= 0 || bodyPartHealth[vitalPart] <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    protected bool Hit()
    {
        if (Random.Range(0, 100) <= attackReaction.dodge)
        {
            string line = "<color=green>" + gameObject.name + " parried the attack!</color>";
            FloatingTextController.CreateFloatingText("Parry", transform, Color.green);
            BattleReport.AddToBattleReport(line);
            return false;
        }
        else if (Random.Range(0, 100) <= attackReaction.block)
        {
            string line = "<color=yellow>" + gameObject.name + " blocked the attack!</color>";
            FloatingTextController.CreateFloatingText("Block", transform, Color.blue);
            BattleReport.AddToBattleReport(line);
            return false;
        }
        else if (Random.Range(0, 100) <= attackReaction.parry)
        {
            string line = "<color=yellow>" + gameObject.name + " parried the attack!</color>";
            FloatingTextController.CreateFloatingText("Parry", transform, Color.yellow);
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
            damageInfo.attackType = "Penetration";
        }
        else
        {
            damageInfo.attackType = "Impact";
        }

        //crit hit
        if (Random.Range(0, 100) <= recievedAttack.skill)
        {
            string line = "<color=yellow>" + recievedAttack.attackerName + " skillfully lands a critical hit with his " + recievedAttack.weapon.name + "!</color>";
            Color color = new Color(0.2F, 0.3F, 0.4F);

            FloatingTextController.CreateFloatingText("Serious Injury", transform, color);
            BattleReport.AddToBattleReport(line);
            damageInfo.damageDealt = enemyAttack;
        }
        else
        {
            float myDefense = armor.protectionValue;
            damageInfo.damageDealt = enemyAttack - myDefense;
        }

        damageInfo.bodyPartID = (int)bodyPart;
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

    //does not guaruntee we will pick the correct part because Dictionaries are arbitrarilly ordered
    protected Parts GetRandomPart()
    {
        int n = Random.Range(0, bodyPartHealth.Count - 1);
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
        public string attackType;
        public int bodyPartID;
    }

    public void LoadBodyPartHealth(Dictionary<Parts, float> savedBodyPartHealth)
    {
        bodyPartHealth = savedBodyPartHealth;
        totalBlood = bodyPartHealth.Sum(x => x.Value);
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
