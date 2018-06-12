using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidCombatSkills : CombatSkills {

    //private Dictionary<HumanoidBody.HumanoidBodyParts, float> bodyPartHealth;

    //monobehavior function that is called when this component is added on top of CombatSkills
    protected override void Start()
    {
        base.Start();

        myWeaponSkills = new Dictionary<Weapon.WeaponType, float>()
        { 
            { Weapon.WeaponType.Axe, 0f },
            { Weapon.WeaponType.Dagger, 0f },
            { Weapon.WeaponType.Hands, 0f },
            { Weapon.WeaponType.Pick, 0f },
            { Weapon.WeaponType.Spear, 0f }
        };
    }

}



