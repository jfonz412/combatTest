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
    private string[] bodyParts = {"Head", "Neck", "LeftArm", "RightArm",
                                    "LHand", "RHand", "Thorax", "Abdomin",
                                    "LeftLeg", "RightLeg", "LFoot", "RFoot" };
    private float[] bodyPartHealth = {  100f, 100f, 100f, 100f,
                                          100f, 100f, 100f, 100f,
                                          100f, 100f, 100f, 100f };
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
        totalBlood = 100f * bodyPartHealth.Length;
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
            int bodyPart = GetRandomPartID();

            DamageInfo damageRecieved = CalculateDamage(recievedAttack, bodyPart);

            if (damageRecieved.damageDealt > 0)
            { 
                Damage(damageRecieved);
            }
            else
            {
                Debug.Log(attacker + "'s " + weapon + " did no damage against " + gameObject.name + "'s " + bodyParts[bodyPart]);
            }
        }
    }

    //need to condense penetrate and impact into one method, refactor this to accomodate for abdomin and organs and all that
    private void Damage(DamageInfo damageInfo)
    {
        float damage = damageInfo.damageDealt;
        int severityID;

        if (damage <= 5)
        {
            severityID = 0;
        }
        else if (damage <= 15)
        {
            severityID = 1;
        }
        else if (damage <= 25)
        {
            severityID = 2;
        }
        else if (damage <= 35)
        {
            severityID = 3;
        }
        else if (damage <= 45)
        {
            severityID = 4;
        }
        else
        {
            severityID = 5;
        }

        damageInfo.severityID = severityID;
        HumanInjuries.DamageMessage(damageInfo);
        bodyPartHealth[damageInfo.bodyPartID] -= damage;

        if (totalBlood > 0)
            StartCoroutine(Bleeding(damage));
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

    private DamageInfo CalculateDamage(AttackInfo recievedAttack, int bodyPartID)
    {
        DamageInfo damageInfo = new DamageInfo();

        //get any armorInfo covering this part
        ArmorInfo armor = GetArmorFrom(bodyParts[bodyPartID]);
        float myDefense = armor.protectionValue;

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

        damageInfo.bodyPartID = bodyPartID;
        damageInfo.damageDealt = enemyAttack - myDefense;
        damageInfo.armorName = armor.name;
        damageInfo.weaponName = recievedAttack.weapon.name;
        damageInfo.victimName = gameObject.name;
        damageInfo.attackerName = recievedAttack.attackerName; //unused right now

        return damageInfo;
    }

    private bool Hit()
    {
        PreventDamageSkills skills = mySkills.GetPreventDamageSkills();

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

    private int GetRandomPartID()
    {
        return Random.Range(0, GetNumberOfParts().Length);
    }

    private int[] GetNumberOfParts()
    {
        int bp = bodyParts.Length;
        int[] validChoices = new int[bp];

        for (int i = 0; i < bp; i++)
        {
            validChoices[i] = i;
        }
        return validChoices;
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
        string[] midsection = { "LeftArm", "RightArm", "LHand", "RHand", "Thorax", "Abdomin", };
        string[] legs = { "LeftLeg", "RightLeg" };
        string[] feet = { "LFoot", "RFoot" }; 

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
}

