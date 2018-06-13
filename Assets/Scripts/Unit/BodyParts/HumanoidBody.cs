using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;

public class HumanoidBody : BodyParts {

    protected override void Start()
    {
        base.Start();

        bodyPartHealth = new Dictionary<Parts, float>()
        {
            { Parts.Head, 100f },
            { Parts.Neck, 100f },
            { Parts.LeftArm, 100f },
            { Parts.RightArm, 100f },
            { Parts.LeftHand, 100f },
            { Parts.RightHand, 100f },
            { Parts.Chest, 100f },
            { Parts.Abdomin, 100f },
            { Parts.LeftLeg, 100f },
            { Parts.RightLeg, 100f },
            { Parts.LeftFoot, 100f },
            { Parts.RightFoot, 100f },
        };

        totalBlood = bodyPartHealth.Sum(x => x.Value);
    }

    //checks for bodyPart in any of the equipment sections
    protected override ArmorInfo GetArmorFrom(string bodyPart)
    {
        string[] helmet = { "Head", "Neck" };
        string[] midsection = { "LeftArm", "RightArm", "LeftHand", "RightHand", "Chest", "Abdomin", };
        string[] legs = { "LeftLeg", "RightLeg" };
        string[] feet = { "LeftFoot", "RightFoot" }; 

        if (ArrayUtility.IndexOf(helmet, bodyPart) >= 0)
        {
            return myArmor[EquipmentSlot.Head];
        }
        else if (ArrayUtility.IndexOf(midsection, bodyPart) >= 0)
        {
            return myArmor[EquipmentSlot.Chest];
        }
        else if (ArrayUtility.IndexOf(legs, bodyPart) >= 0)
        {
            return myArmor[EquipmentSlot.Legs];
        }
        else if (ArrayUtility.IndexOf(feet, bodyPart) >= 0)
        {
            return myArmor[EquipmentSlot.Feet];
        }
        else
        {
            Debug.LogError("Bodypart not found!");
            return myArmor[0];
        }
    }

    public override float OverallHealth()
    {
        float leftArm = bodyPartHealth[Parts.LeftArm];
        float rightArm = bodyPartHealth[Parts.RightArm];
        float leftHand = bodyPartHealth[Parts.LeftHand];
        float rightHand = bodyPartHealth[Parts.RightHand];
        float leftLeg = bodyPartHealth[Parts.LeftLeg];
        float rightLeg = bodyPartHealth[Parts.RightLeg];
        float leftFoot = bodyPartHealth[Parts.LeftFoot];
        float rightFoot = bodyPartHealth[Parts.RightFoot];
        float head = bodyPartHealth[Parts.Head];
        float neck = bodyPartHealth[Parts.Neck];
        float chest = bodyPartHealth[Parts.Chest];
        float abdomin = bodyPartHealth[Parts.Abdomin];

        return baseHealth * ((leftArm + rightArm + leftHand + rightHand + leftLeg + rightLeg + leftFoot + rightFoot +
                                 abdomin + chest + neck + head / 12) * 0.01f);
    }

    protected override void GetArmor(Equipment oldItem, Equipment newItem)
    {
        if (newItem == null)
            return;

        if (newItem.GetType() == typeof(Armor))
        {
            Armor armor = newItem as Armor; //make this a dictionary with the key as the aeeuip sslot and the value the armor info
            ArmorInfo armorInfo = armor.GetArmorInfo();

            myArmor[armorInfo.armorType] = armorInfo;
            Debug.Log("Loading " + armorInfo.name + " for " + gameObject.name);
        }
    }
}


