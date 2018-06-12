using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSkills : MonoBehaviour {
    public float speed = 2; //TEMPORARY

    [SerializeField]
    protected float baseBlock, baseParry, baseDodge, baseHealth;
    [SerializeField]
    protected float strength, agility;

    protected Dictionary<Weapon.WeaponType, float> myWeaponSkills; //will be overwritted by inherited classes
    protected BodyParts body;

    protected virtual void Start()
    {
        body = GetComponent<BodyParts>();
    }

    public virtual AttackInfo RequestAttackInfo(Weapon currentWeapon)
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

    //copies defense skills and gives it to the body DefenseSkills is not public so we use a different struct
    public AttackReactionSkills GetAttackReactionSkills()
    {
        AttackReactionSkills a = new AttackReactionSkills();
        a.block = baseBlock;
        a.dodge = baseDodge;
        a.parry = baseParry;

        return a;
    }

    protected virtual float WeaponSkill(Weapon.WeaponType weaponType)
    {
        return body.OverallHealth() * myWeaponSkills[weaponType];
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

//this is passed to the human body to see if unit can react to the attack without taking damage
public struct AttackReactionSkills
{
    public float dodge, block, parry;
}