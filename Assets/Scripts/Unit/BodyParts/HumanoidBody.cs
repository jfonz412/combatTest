using System.Collections;
using UnityEditor;
using UnityEngine;

public class HumanoidBody : BodyParts {
    private EquipmentManager equipment;
    private Skills mySkills;

    //dictionaries cannot be serialized in unity so we have to seperate them (at least at some point) into arrays
    //these will be linked by their indexes
    private string[] bodyParts = {"Head", "Neck", "LeftArm", "RightArm",
                                    "LHand", "RHand", "Thorax", "Abdomin",
                                    "LeftLeg", "RightLeg", "LFoot", "RFoot" };
    private float[] bodyPartHealth = {  100f, 100f, 100f, 100f,
                                          100f, 100f, 100f, 100f,
                                          100f, 100f, 100f, 100f };
    private float totalBlood;


    private void Start ()
    {
        equipment = GetComponent<EquipmentManager>();
        mySkills = GetComponent<Skills>();

        //totalBlood = 100f * bodyPartHealth.Length;
        totalBlood = 100f;
	}

    public override void RecieveAttack(AttackInfo recievedAttack)
    {
        base.RecieveAttack(recievedAttack);
        DetermineImpact(recievedAttack);
    }

    private void DetermineImpact(AttackInfo recievedAttack)
    {
        if (Hit())
        {
            int bodyPart = GetBodyPartID();

            float damageRecieved = CalculateDamage(recievedAttack, bodyPart);

            if (damageRecieved > 0)
            {
                if(recievedAttack.weapon.sharpness > .60)
                {
                    Damage(bodyPart, damageRecieved, "Penetration");
                }
                else
                {
                    Damage(bodyPart, damageRecieved, "Impact");
                }
            }
            else
            {
                Debug.Log(recievedAttack.weapon.name + " did no damage against " + gameObject.name + "'s " + bodyParts[bodyPart]);
            }
        }
    }

    //need to condense penetrate and impact into one method, refactor this to accomodate for abdomin and organs and all that
    private void Damage(int bodyPartID, float damage, string damageType)
    {
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

        Debug.Log(severityID);
        if (damageType == "Penetration")
        {
            HumanInjuries.PenetrationDamage(severityID, bodyPartID, gameObject.name);
        }
        else
        {
            HumanInjuries.ImpactDamage(severityID, bodyPartID, gameObject.name);
        }

        bodyPartHealth[bodyPartID] -= damage;

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

    private float CalculateDamage(AttackInfo recievedAttack, int bodyPartID)
    {
        //get any armorInfo covering this part
        float armorProtection = equipment.EquipmentFromSlot(GetArmorSlotNum(bodyParts[bodyPartID])).GetProtectionValue();
        float weaponHardness = recievedAttack.weapon.hardnessValue;

        float enemyAttack = weaponHardness + recievedAttack.force;
        float myDefense = armorProtection;

        return enemyAttack - myDefense;
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

    private int GetBodyPartID()
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

    private int GetArmorSlotNum(string bodyPart)
    {
        string[] helmet = { "Head", "Neck" };
        string[] midsection = { "LeftArm", "RightArm", "LHand", "RHand", "Thorax", "Abdomin", };
        string[] legs = { "LeftLeg", "RightLeg" };
        string[] feet = { "LFoot", "RFoot" }; 

        if (ArrayUtility.IndexOf(midsection, bodyPart) >= 0)
        {
            return (int)EquipmentSlot.Chest;
        }
        else if (ArrayUtility.IndexOf(legs, bodyPart) >= 0)
        {
            return (int)EquipmentSlot.Legs;
        }
        else if (ArrayUtility.IndexOf(feet, bodyPart) >= 0)
        {
            return (int)EquipmentSlot.Feet;
        }
        else if (ArrayUtility.IndexOf(helmet, bodyPart) >= 0)
        {
            return (int)EquipmentSlot.Head;
        }
        else
        {
            return -1;
        }
    }
}
