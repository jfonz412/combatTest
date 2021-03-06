﻿using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;

public class BushBansheeBody : BodyPartController
{
    /*
    protected override void Start()
    {
        base.Start();
        if (bodyPartDamage == null)
            bodyPartDamage = new Dictionary<Parts, int>()
            {
                { Parts.LeftArm, 0 },
                { Parts.RightArm, 0 },
                { Parts.LeftHand, 0 },
                { Parts.RightHand, 0 },
                { Parts.Chest, 0 },
                { Parts.Abdomin, 0 },
                { Parts.LeftLeg, 0 },
                { Parts.RightLeg, 0 },
                { Parts.LeftFoot, 0 },
                { Parts.RightFoot, 0 },
            };

        vitalParts = new Parts[] { Parts.Abdomin, Parts.Chest };
    }

    //checks for bodyPart in any of the equipment sections
    protected override ArmorInfo GetArmorFrom(string bodyPart)
    {
        string[] midsection = { "LeftArm", "RightArm", "LeftHand", "RightHand", "Chest", "Abdomin", };
        string[] legs = { "LeftLeg", "RightLeg" };
        string[] feet = { "LeftFoot", "RightFoot" };

        if (ArrayUtility.IndexOf(midsection, bodyPart) >= 0)
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
    */
}

