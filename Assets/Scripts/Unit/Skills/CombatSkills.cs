using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSkills : MonoBehaviour {
    public float speed = 2; //TEMPORARY

    protected BodyParts body;

    protected Dictionary<Weapon.WeaponType, int> myWeaponSkillLevels; //will be overwritted by inherited classes
    protected Dictionary<Weapon.WeaponType, float> myWeaponSkillExperience; //will be overwritted by inherited classes

    public enum CombatSkill { Block, Parry, Dodge, Strength, Agility, Willpower }

    private Dictionary<CombatSkill, int> mySkillLevels = 
        new Dictionary<CombatSkill, int>()
        {
            { CombatSkill.Agility, 0 },
            { CombatSkill.Block, 0 },
            { CombatSkill.Dodge, 0 },
            { CombatSkill.Parry, 0 },
            { CombatSkill.Strength, 0 },
            { CombatSkill.Willpower, 0 }
        };
    private Dictionary<CombatSkill, float> mySkillExperience =
        new Dictionary<CombatSkill, float>()
        {
            { CombatSkill.Agility, 0f },
            { CombatSkill.Block, 0f },
            { CombatSkill.Dodge, 0f },
            { CombatSkill.Parry, 0f },
            { CombatSkill.Strength, 0f },
            { CombatSkill.Willpower, 0f }
        };

    public delegate void OnSkillGained();
    public OnSkillGained onSkillGained;

    protected virtual void Start()
    {
        body = GetComponent<BodyParts>();
    }

    public virtual AttackInfo RequestAttackInfo(Weapon currentWeapon)
    {
        AttackInfo info = new AttackInfo();
        WeaponInfo weapon = currentWeapon.RequestWeaponInfo();
        float associatedSkill = WeaponSkillEffectiveness(weapon.weaponType);

        info.attackerName = gameObject.name;
        info.weapon = weapon;
        info.speed = weapon.speed; // - strength and skill
        info.force = mySkillLevels[CombatSkill.Strength] + associatedSkill + weapon.weight;
        info.skill = associatedSkill; //should eventually factor against enemiy's defensive skills (dodge, parry, block, absoarb?)

        WeaponExperienceGain(weapon.weaponType, 50f);
        return info;
    }

    public void ExperienceGain(CombatSkill skill, float experience) //skill, float
    {
        mySkillExperience[skill] += experience;
        if (mySkillExperience[skill] >= (100 * mySkillLevels[skill]))
        {
            mySkillLevels[skill]++;
            mySkillExperience[skill] = 0f;
            onSkillGained();
            Debug.Log("LEVEL UP: " + mySkillLevels[skill] + "!!!");
        }
    }

    protected void WeaponExperienceGain(Weapon.WeaponType weapon, float experience)
    {
        myWeaponSkillExperience[weapon] += experience;
        if (myWeaponSkillExperience[weapon] >= (100 * myWeaponSkillLevels[weapon]))
        {
            myWeaponSkillLevels[weapon]++;
            myWeaponSkillExperience[weapon] = 0f;
            //onSkillGained(); //not needed for now
            Debug.Log("LEVEL UP: " + myWeaponSkillLevels[weapon] + "!!!");
        }
    }

    //copies defense skills and gives it to the body DefenseSkills is not public so we use a different struct
    public AttackReactionSkills GetAttackReactionSkills()
    {
        AttackReactionSkills a = new AttackReactionSkills();
        a.block = mySkillLevels[CombatSkill.Block];
        a.dodge = mySkillLevels[CombatSkill.Dodge];
        a.parry = mySkillLevels[CombatSkill.Parry];
        a.will  = mySkillLevels[CombatSkill.Willpower];
        return a;
    }

    protected virtual float WeaponSkillEffectiveness(Weapon.WeaponType weaponType)
    { 
        Debug.Log(body.OverallHealth() + " * " + myWeaponSkillLevels[weaponType] + " = " + body.OverallHealth() * myWeaponSkillLevels[weaponType]);
        return body.OverallHealth() * myWeaponSkillLevels[weaponType];
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
    public float dodge, block, parry, will;
}