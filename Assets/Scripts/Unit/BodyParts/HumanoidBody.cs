using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HumanoidBody : BodyParts {
    private EquipmentManager equipment;
    private Skills mySkills;

    //maybe make this a dictionary, key will be the string, value will have a BodyPartInfo
    protected string[] bodyParts = {"Head", "Neck", "LeftArm", "RightArm",
                                    "LHand", "RHand", "Thorax", "Abdomin",
                                    "LeftLeg", "RightLeg", "LFoot", "RFoot" };

    

    private void Start ()
    {
        equipment = GetComponent<EquipmentManager>();
        mySkills = GetComponent<Skills>();
	}

    public override void RecieveAttack(AttackInfo recievedAttack)
    {
        base.RecieveAttack(recievedAttack);
        DetermineImpact(recievedAttack);
        Debug.Log("HumanBody attacked");
    }

    private void DetermineImpact(AttackInfo recievedAttack)
    {


        if (Hit())
        {
            string bodyPart = bodyParts[PickBodyPart()];

            float damageRecieved = CalculateDamage(recievedAttack, bodyPart);

            if (damageRecieved > 0)
            {
                if(recievedAttack.weapon.sharpness > .60)
                {
                    //PenetrationDamage(bodyPart, damage)
                }
                else
                {
                    //ImpactDamage(bodyPart, damage);
                }
            }

        }
    }

    private void PenetrationDamage(string bodyPart, float damage)
    {
        //determine severity of injury
        //pass the string and severity of damage to skills InjureSkillsAssociatedWith(string bodyPart, string/enum/float severity)
        //save injury to the body, check if we are dead/dazed/bleeding out etc.
        //instantiate floating text
    }

    private void ImpactDamage(string bodyPart, float damage)
    {
        //determine severity of injury
        //pass the string and severity of damage to skills InjureSkillsAssociatedWith(string bodyPart, string/enum/float severity)
        //save injury to the body, check if we are dead/dazed/bleeding out etc.
        //instantiate floating text
    }

    private float CalculateDamage(AttackInfo recievedAttack, string bodyPart)
    {
        //get any armorInfo covering this part
        float armorProtection = equipment.EquipmentFromSlot(GetArmorSlotNum(bodyPart)).GetProtectionValue();
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

    private int PickBodyPart()
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
        //string[] noArmor = { "Neck" }; ?
        string[] midsection = { "LeftArm", "RightArm", "LHand", "RHand", "Thorax", "Abdomin", };
        string[] legs = { "LeftLeg", "RightLeg" };
        string[] feet = { "LFoot", "RFoot" };

        string[] stringArray = { "text1", "someothertext"};

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
