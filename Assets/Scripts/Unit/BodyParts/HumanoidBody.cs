using System.Collections.Generic;
using System.Collections;
using UnityEditor;
using UnityEngine;

public class HumanoidBody : BodyParts {
    private EquipmentManager equipment;
    private Skills mySkills;

    //SHOULD EVENTUALLY MAKE A DELEGATION THAT WILL GIVE THIS SCRIPT ARMOR INFO 
    //WHEN EQUIPPING AND UNEQUIPPING ARMOR, LIKE ATTACK CONTROLLER FOR WEAPON

    //dictionaries cannot be serialized in unity so we have to seperate them (at least at some point) into arrays
    //these will be linked by their indexes
    public enum HumanoidBodyParts { Head, Neck, LeftArm, RightArm, LeftHand, RightHand, Chest, Abdomin, LeftLeg, RightLeg, LeftFoot, RightFoot }
    public Dictionary<HumanoidBodyParts, float> bodyPartHealth = new Dictionary<HumanoidBodyParts, float>()
    {
        { HumanoidBodyParts.Head, 100f },
        { HumanoidBodyParts.Neck, 100f },
        { HumanoidBodyParts.LeftArm, 100f },
        { HumanoidBodyParts.RightArm, 100f },
        { HumanoidBodyParts.LeftHand, 100f },
        { HumanoidBodyParts.RightHand, 100f },
        { HumanoidBodyParts.Chest, 100f },
        { HumanoidBodyParts.Abdomin, 100f },
        { HumanoidBodyParts.LeftLeg, 100f },
        { HumanoidBodyParts.RightLeg, 100f },
        { HumanoidBodyParts.LeftFoot, 100f },
        { HumanoidBodyParts.RightFoot, 100f },
    };

    private float totalBlood;

    private ArmorInfo[] myArmor = new ArmorInfo[4]; //helmet, upper body, lower body, feet

    private void Awake()
    {
        equipment = GetComponent<EquipmentManager>();
        equipment.onEquipmentChanged += GetArmor;
    }

    private void Start ()
    {
        mySkills = GetComponent<Skills>();
        totalBlood = 100f * System.Enum.GetValues(typeof(HumanoidBodyParts)).Length;
	}

    public override void RecieveAttack(AttackInfo recievedAttack)
    {
        base.RecieveAttack(recievedAttack);
        DetermineImpact(recievedAttack);
    }

    private void DetermineImpact(AttackInfo recievedAttack)
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
    private void Damage(DamageInfo damageInfo)
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
        DamageBodyPart((HumanoidBodyParts)damageInfo.bodyPartID, damage);

        if (totalBlood > 0)
            StartCoroutine(Bleeding(damage));
    }

    public void DamageBodyPart(HumanoidBodyParts bodyPart, float damage)
    {
        bodyPartHealth[bodyPart] -= damage;

        if (bodyPartHealth[bodyPart] < 0f)
            bodyPartHealth[bodyPart] = 0f;

        Debug.Log(bodyPart + "health is " + bodyPartHealth[bodyPart]);
    }

    private IEnumerator Bleeding(float damage)
    {
        while (damage > 0)
        {
            totalBlood -= damage;
            Debug.Log(gameObject.name + " just bled " + damage + " damage. Blood remaining: " + totalBlood);
            damage = (damage / 2) - 1f;
            yield return new WaitForSeconds(1f);            
        }

        if(totalBlood <= 0)
        {
            GetComponent<NPCDeath>().Die();
        }

        yield break;
    }


    private DamageInfo CalculateDamage(AttackInfo recievedAttack)
    {
        DamageInfo damageInfo = new DamageInfo();

        HumanoidBodyParts bodyPart = GetRandomPart();
        Debug.Log("enum is " + bodyPart.ToString());
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

    private bool Hit()
    {
        AttackReactionSkills skills = mySkills.GetAttackReactionSkills();

        if(Random.Range(0,100) <= skills.dodge)
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

    private HumanoidBodyParts GetRandomPart()
    {
        return (HumanoidBodyParts)Random.Range(0, System.Enum.GetValues(typeof(HumanoidBodyParts)).Length);
    }


    public void GetArmor(Equipment oldItem, Equipment newItem)
    {
        if (newItem == null)
            return;

        if(newItem.GetType() == typeof(Armor))
        {
            Armor armor = newItem as Armor;
            ArmorInfo armorInfo = armor.GetArmorInfo();
            int slot = (int)armorInfo.armorType;

            if (slot < myArmor.Length - 1) //makes sure we don't try and add weapons
                myArmor[slot] = armorInfo;
        }
    }

    //checks for bodyPart in any of the equipment sections
    private ArmorInfo GetArmorFrom(string bodyPart)
    {
        string[] helmet = { "Head", "Neck" };
        string[] midsection = { "LeftArm", "RightArm", "LeftHand", "RightHand", "Chest", "Abdomin", };
        string[] legs = { "LeftLeg", "RightLeg" };
        string[] feet = { "LeftFoot", "RightFoot" }; 

        if (ArrayUtility.IndexOf(helmet, bodyPart) >= 0)
        {
            return myArmor[0];
        }
        else if (ArrayUtility.IndexOf(midsection, bodyPart) >= 0)
        {
            return myArmor[1];
        }
        else if (ArrayUtility.IndexOf(legs, bodyPart) >= 0)
        {
            return myArmor[2];
        }
        else if (ArrayUtility.IndexOf(feet, bodyPart) >= 0)
        {
            return myArmor[3];
        }
        else
        {
            Debug.LogError("Bodypart not found!");
            return myArmor[0];
        }
    }

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


