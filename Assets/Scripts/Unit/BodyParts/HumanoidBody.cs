using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;

public class HumanoidBody : BodyParts {

    protected override void Start()
    {
        base.Start();

        if(bodyPartHealth == null)
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

        vitalParts = new Parts[] { Parts.Head, Parts.Neck, Parts.Abdomin, Parts.Chest };
        totalBlood = bodyPartHealth.Sum(x => x.Value);

        if (onHealthLoaded != null)
            onHealthLoaded.Invoke();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("neck is " + bodyPartHealth[Parts.Neck] + " for " + gameObject.name);
        }
    }

    //checks for bodyPart in any of the equipment sections
    protected override ArmorInfo GetArmorFrom(string bodyPart)
    {
        //Debug.Log(gameObject + " " + bodyPart);
        string[] helmet = { "Head", "Neck" };
        string[] midsection = { "LeftArm", "RightArm", "Chest", "Abdomin", };
        string[] hands = { "LeftHand", "RightHand" };
        string[] legs = { "LeftLeg", "RightLeg" };
        string[] feet = { "LeftFoot", "RightFoot" }; 

        if (ArrayUtility.IndexOf(helmet, bodyPart) >= 0)
        {
            return myArmor[EquipmentSlot.Head];
        }
        else if (ArrayUtility.IndexOf(hands, bodyPart) >= 0)
        {
            return myArmor[EquipmentSlot.Hands];
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

    protected override void GetArmor(Equipment oldItem, Equipment newItem)
    {
        if (newItem == null)
            return;

        if (newItem.GetType() == typeof(Armor))
        {
            Armor armor = newItem as Armor; //make this a dictionary with the key as the aeeuip sslot and the value the armor info
            ArmorInfo armorInfo = armor.GetArmorInfo();

            myArmor[armorInfo.armorType] = armorInfo;
        }
    }
}


