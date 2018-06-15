using System.Collections.Generic;

public class HumanoidCombatSkills : CombatSkills {
    protected override void Start()
    {
        base.Start();

        myWeaponSkillLevels = new Dictionary<Weapon.WeaponType, int>()
        { 
            { Weapon.WeaponType.Axe, 0 },
            { Weapon.WeaponType.Dagger, 0 },
            { Weapon.WeaponType.Hands, 0 },
            { Weapon.WeaponType.Pick, 0 },
            { Weapon.WeaponType.Spear, 0 }
        };

        myWeaponSkillExperience = new Dictionary<Weapon.WeaponType, float>()
        {
            { Weapon.WeaponType.Axe, 0f },
            { Weapon.WeaponType.Dagger, 0f },
            { Weapon.WeaponType.Hands, 0f },
            { Weapon.WeaponType.Pick, 0f },
            { Weapon.WeaponType.Spear, 0f }
        };
    }
}



