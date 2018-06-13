using System.Collections.Generic;
using System.Collections;
using UnityEngine;


public class BodyParts : MonoBehaviour {
    public enum Parts { Head, Neck, LeftArm, RightArm, LeftHand, RightHand, Chest, Abdomin, LeftLeg, RightLeg, LeftFoot, RightFoot }
    public Dictionary<Parts, float> bodyPartHealth;

    protected EquipmentManager equipment;
    protected CombatSkills mySkills;
    protected Dictionary<EquipmentSlot, ArmorInfo> myArmor = new Dictionary<EquipmentSlot, ArmorInfo>();

    protected float totalBlood; //100 * number of bodyparts
    protected float baseHealth = 100f;

    protected void Awake()
    {
        equipment = GetComponent<EquipmentManager>();
        equipment.onEquipmentChanged += GetArmor;
    }

    protected virtual void Start()
    {
        mySkills = GetComponent<CombatSkills>();
    }

    public virtual void RecieveAttack(AttackInfo recievedAttack)
    {
        DetermineImpact(recievedAttack);
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
                Debug.Log(attacker + " did no damage to " + gameObject.name + ".");
            }
        }
    }

    //need to condense penetrate and impact into one method, refactor this to accomodate for abdomin and organs and all that
    protected void Damage(DamageInfo damageInfo)
    {
        float damage = damageInfo.damageDealt;
        int severityID;

        if (damage <= 50)
        {
            severityID = 0;
        }
        else if (damage <= 70)
        {
            severityID = 1;
        }
        else if (damage <= 90)
        {
            severityID = 2;
        }
        else if (damage <= 100)
        {
            severityID = 3;
        }
        else if (damage <= 120)
        {
            severityID = 4;
        }
        else
        {
            severityID = 5;
        }

        damageInfo.severityID = severityID;
        HumanInjuries.DamageMessage(damageInfo);
        DamageBodyPart((BodyParts.Parts)damageInfo.bodyPartID, damage);

        if (totalBlood > 0)
            StartCoroutine(Bleeding(damage));
    }

    protected void DamageBodyPart(BodyParts.Parts bodyPart, float damage)
    {
        bodyPartHealth[bodyPart] -= damage;

        if (bodyPartHealth[bodyPart] < 0f)
            bodyPartHealth[bodyPart] = 0f;

        Debug.Log(bodyPart + "health is " + bodyPartHealth[bodyPart]);
    }

    protected IEnumerator Bleeding(float damage)
    {
        while (damage > 0)
        {
            totalBlood -= damage;
            Debug.Log(gameObject.name + " just bled " + damage + " damage. Blood remaining: " + totalBlood);
            damage = (damage / 2) - 1f;
            yield return new WaitForSeconds(1f);
        }

        if (totalBlood <= 0)
        {
            GetComponent<NPCDeath>().Die();
        }

        yield break;
    }

    protected bool Hit()
    {
        AttackReactionSkills skills = mySkills.GetAttackReactionSkills();

        if (Random.Range(0, 100) <= skills.dodge)
        {
            Debug.Log(gameObject.name + " dodged the attack!");
            return false;
        }
        else if (Random.Range(0, 100) <= skills.block)
        {
            Debug.Log(gameObject.name + " blocked the attack!");
            return false;
        }
        else if (Random.Range(0, 100) <= skills.parry)
        {
            Debug.Log(gameObject.name + " parried the attack!");
            return false;
        }

        return true;
    }

    protected DamageInfo CalculateDamage(AttackInfo recievedAttack)
    {
        DamageInfo damageInfo = new DamageInfo();

        Parts bodyPart = GetRandomPart();
        Debug.Log("enum is " + bodyPart.ToString());
        ArmorInfo armor = GetArmorFrom(bodyPart.ToString());
        Debug.Log("armor is " + armor.name);
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

        if (Random.Range(0, 100) <= recievedAttack.skill)
        {
            Debug.Log(recievedAttack.attackerName + " skillfully lands a critical hit with his " + recievedAttack.weapon.name + "!");
            damageInfo.damageDealt = enemyAttack;
        }
        else
        {
            float myDefense = armor.protectionValue;
            damageInfo.damageDealt = enemyAttack - myDefense;
        }


        //DamageSkillCheck(recievedAttack);

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
        return (Parts)Random.Range(0, bodyPartHealth.Count); 
    }

    public virtual float OverallHealth()
    {
        Debug.LogError("Base method should not be called here!");
        return 0;
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
        public int severityID;
        public string attackType;
        public int bodyPartID;
    }

    public void LoadSavedDamage(float[] savedHealth)
    {
        //cycle through the bodyPartHealth array and load that into the dictionary
    }

    public float[] GetBodyPartDamage()
    {
        //add all values from bodyPartHealth into an array and give that to the dataController
        return new float[0];
    }
}
