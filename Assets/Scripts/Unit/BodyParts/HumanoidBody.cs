using System.Collections.Generic;
using System.Collections;
using UnityEditor;
using UnityEngine;

public class HumanoidBody : BodyParts {
    private EquipmentManager equipment;

    private ArmorInfo[] myArmor = new ArmorInfo[4]; //helmet, upper body, lower body, feet

    private void Awake()
    {
        equipment = GetComponent<EquipmentManager>();
        equipment.onEquipmentChanged += GetArmor;
    }

    protected override void Start()
    {
        base.Start();

        bodyPartHealth = new Dictionary<BodyParts.Parts, float>()
        {
            { BodyParts.Parts.Head, 100f },
            { BodyParts.Parts.Neck, 100f },
            { BodyParts.Parts.LeftArm, 100f },
            { BodyParts.Parts.RightArm, 100f },
            { BodyParts.Parts.LeftHand, 100f },
            { BodyParts.Parts.RightHand, 100f },
            { BodyParts.Parts.Chest, 100f },
            { BodyParts.Parts.Abdomin, 100f },
            { BodyParts.Parts.LeftLeg, 100f },
            { BodyParts.Parts.RightLeg, 100f },
            { BodyParts.Parts.LeftFoot, 100f },
            { BodyParts.Parts.RightFoot, 100f },
        };

        totalBlood = 100f * bodyPartHealth.Count;
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
        DamageBodyPart((BodyParts.Parts)damageInfo.bodyPartID, damage);

        if (totalBlood > 0)
            StartCoroutine(Bleeding(damage));
    }

    public void DamageBodyPart(BodyParts.Parts bodyPart, float damage)
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

        BodyParts.Parts bodyPart = GetRandomPart();
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

    private BodyParts.Parts GetRandomPart()
    {
        return (BodyParts.Parts)Random.Range(0, bodyPartHealth.Count); //does not guaruntee we will pick the correct part because Dictionaries are arbitrarilly ordered
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

    public override float OverallHealth()
    {
        Debug.Log(bodyPartHealth);
        float leftArm = bodyPartHealth[BodyParts.Parts.LeftArm];
        float rightArm = bodyPartHealth[BodyParts.Parts.RightArm];
        float leftHand = bodyPartHealth[BodyParts.Parts.LeftHand];
        float rightHand = bodyPartHealth[BodyParts.Parts.RightHand];
        float leftLeg = bodyPartHealth[BodyParts.Parts.LeftLeg];
        float rightLeg = bodyPartHealth[BodyParts.Parts.RightLeg];
        float leftFoot = bodyPartHealth[BodyParts.Parts.LeftFoot];
        float rightFoot = bodyPartHealth[BodyParts.Parts.RightFoot];
        float head = bodyPartHealth[BodyParts.Parts.Head];
        float neck = bodyPartHealth[BodyParts.Parts.Neck];
        float chest = bodyPartHealth[BodyParts.Parts.Chest];
        float abdomin = bodyPartHealth[BodyParts.Parts.Abdomin];

        return baseHealth * ((leftArm + rightArm + leftHand + rightHand + leftLeg + rightLeg + leftFoot + rightFoot +
                                 abdomin + chest + neck + head / 12) * 0.01f);
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


