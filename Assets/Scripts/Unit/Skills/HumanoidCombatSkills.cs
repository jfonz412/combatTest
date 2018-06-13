using System.Collections.Generic;

public class HumanoidCombatSkills : CombatSkills {
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



