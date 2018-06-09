using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skills : MonoBehaviour {
    //private Dictionary<int, string> dict = new Dictionary<int, string>();
    [SerializeField]
    private float dagger, spear, axe, pick, unarmed;
    [SerializeField]
    private float dodge, block, parry;
    [SerializeField]
    private float strength, agility;

    private BodyParts body;

    private void Start()
    {
        body = GetComponent<BodyParts>();
    }

    public AttackInfo RequestAttackInfo(Weapon currentWeapon)
    {
        AttackInfo info = new AttackInfo();
        WeaponInfo weapon = currentWeapon.RequestWeaponInfo();
        float associatedSkill = WeaponSkill(weapon.weaponType);

        info.attackerName = gameObject.name;
        info.weapon = weapon;
        info.speed = weapon.speed; // - strength and skill
        info.force = strength + associatedSkill + weapon.weight;
        info.skill = associatedSkill; //will factor against enemiy's defensive skills (dodge, parry, block, absoarb?)

        //add experience to weapon type/action skill
        return info;
    }

    public PreventDamageSkills GetPreventDamageSkills()
    {
        PreventDamageSkills p = new PreventDamageSkills();
        p.block = block;
        p.dodge = dodge;
        p.parry = parry;

        return p;
    }

    private float WeaponSkill(Weapon.WeaponType weaponType)
    {
        float skill;

        switch (weaponType)
        {
            case Weapon.WeaponType.Dagger:
                skill = dagger;
                break;
            case Weapon.WeaponType.Axe:
                skill = axe;
                break;
            case Weapon.WeaponType.Spear:
                skill = spear;
                break;
            case Weapon.WeaponType.Pick:
                skill = pick;
                break;
            case Weapon.WeaponType.Hands:
                skill = pick;
                break;
            default:
                skill = 0;
                Debug.LogError("Invalid Weapon Type");
                break;
        }

        return skill;
    }
}

public struct AttackInfo
{
    public string attackerName;
    public WeaponInfo weapon;
    public float force;
    public float skill;
    public float speed;
}

public struct PreventDamageSkills
{
    public float dodge, block, parry;
}
