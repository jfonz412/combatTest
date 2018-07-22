using System.Collections.Generic;
using UnityEngine;

public class CombatSkills : MonoBehaviour {

    private BodyPartController body;
    private EquipmentManager equipmentManager;

    //this will be used to set the stats
    [SerializeField]
    private List<Item.WeaponType> defaultWeapons = new List<Item.WeaponType>();
    [SerializeField]
    private List<int> defaultWeaponStats = new List<int>();
    [SerializeField]
    private List<CombatSkill> defaultSkills = new List<CombatSkill>();
    [SerializeField]
    private List<int> defaultSkillStats = new List<int>();

    public int statusResistance { get { return mySkillLevels[CombatSkill.Willpower]; } }

    #region Skills

    private Dictionary<Item.WeaponType, int> myWeaponSkillLevels = new Dictionary<Item.WeaponType, int>()
        {
            { Item.WeaponType.Axe, 0 },
            { Item.WeaponType.Dagger, 0 },
            { Item.WeaponType.Hands, 0 },
            { Item.WeaponType.Offhand, 0 },
            { Item.WeaponType.Pick, 0 },
            { Item.WeaponType.Spear, 0 },
            { Item.WeaponType.Misc, 0 }
        };

    private Dictionary<Item.WeaponType, float> myWeaponSkillExperience = new Dictionary<Item.WeaponType, float>()
        {
            { Item.WeaponType.Axe, 0f },
            { Item.WeaponType.Dagger, 0f },
            { Item.WeaponType.Hands, 0f },
            { Item.WeaponType.Offhand, 0f },
            { Item.WeaponType.Pick, 0f },
            { Item.WeaponType.Spear, 0f },
            { Item.WeaponType.Misc, 0 }
        };

    public enum CombatSkill { Block, Parry, Dodge, Strength, Agility, Willpower, Speed }

    private Dictionary<CombatSkill, int> mySkillLevels = new Dictionary<CombatSkill, int>()
        {
            { CombatSkill.Agility, 0 }, //don't think agility, willpower are used right now
            { CombatSkill.Block, 0 },
            { CombatSkill.Dodge, 0 },
            { CombatSkill.Parry, 0 }, 
            { CombatSkill.Strength, 0 },
            { CombatSkill.Willpower, 0 },
            {CombatSkill.Speed, 0 }
        };

    private Dictionary<CombatSkill, float> mySkillExperience = new Dictionary<CombatSkill, float>()
        {
            { CombatSkill.Agility, 0f },
            { CombatSkill.Block, 0f },
            { CombatSkill.Dodge, 0f },
            { CombatSkill.Parry, 0f },
            { CombatSkill.Strength, 0f },
            { CombatSkill.Willpower, 0f },
            { CombatSkill.Speed, 0f }
        };

    public delegate void OnSkillGained();
    public OnSkillGained onSkillGained;
#endregion

    private void Awake()
    {
        LoadDefaultSkills();
    }

    private void Start()
    {
        body = GetComponent<BodyPartController>();
        //get offhand from body
        //equipmentManager = GetComponent<EquipmentManager>();
        //equipmentManager.onEquipmentChanged += SwapOffHand;
    }

    public float GetCurrentMoveSpeed()
    {
        return mySkillLevels[CombatSkill.Speed] * body.OverallHealth();
    }

    public AttackInfo RequestAttackInfo(Item currentWeapon)
    {
        AttackInfo info = new AttackInfo();
        //ItemInfo weapon = currentWeapon.RequestItemInfo();
        float weaponSkill = GetWeaponSkill(currentWeapon.myWeaponType);

        info.attackerName = gameObject.name;
        info.weapon = currentWeapon;
        info.speed = currentWeapon.weight; // - strength and skill
        info.force = mySkillLevels[CombatSkill.Strength] + weaponSkill + currentWeapon.weight;
        info.skill = weaponSkill; //should eventually factor against enemiy's defensive skills (dodge, parry, block, absoarb?)

        WeaponExperienceGain(currentWeapon.myWeaponType, 50f);
        return info;
    }

    public void ExperienceGain(CombatSkill skill, float experience) //skill, float
    {
        mySkillExperience[skill] += experience;
        if (mySkillExperience[skill] >= (100 * mySkillLevels[skill]))
        {
            mySkillLevels[skill]++;
            mySkillExperience[skill] = 0f;
            SkillGainCallback();
            //Debug.Log("LEVEL UP: " + mySkillLevels[skill] + "!!!");
        }
    }

    //this will only happen here
    private void WeaponExperienceGain(Item.WeaponType weapon, float experience)
    {
        myWeaponSkillExperience[weapon] += experience;
        if (myWeaponSkillExperience[weapon] >= (100 * myWeaponSkillLevels[weapon]))
        {
            myWeaponSkillLevels[weapon]++;
            myWeaponSkillExperience[weapon] = 0f;
            SkillGainCallback();
            //Debug.Log("LEVEL UP: " + weapon + myWeaponSkillLevels[weapon] + "!!!");
        }
    }

    private void SkillGainCallback()
    {
        if (onSkillGained != null)
            onSkillGained.Invoke();
    }

    private float GetWeaponSkill(Item.WeaponType weaponType)
    {
        //Debug.Log(body.OverallHealth() + " * " + myWeaponSkillLevels[weaponType] + " = " + body.OverallHealth() * myWeaponSkillLevels[weaponType]);
        return body.OverallHealth() * myWeaponSkillLevels[weaponType];
    }

    //copies defense skills and gives it to the body DefenseSkills is not public so we use a different struct
    public AttackReactionSkills GetAttackReactionSkills()
    {
        AttackReactionSkills a = new AttackReactionSkills();

        float blockMod = 0f;
        Debug.Log("offhand has no effect on blocking right now");
        /*
        if (equippedOffHand != null)
            blockMod = equippedOffHand.hardnessValue;
        */

        a.block = mySkillLevels[CombatSkill.Block] + blockMod;
        a.dodge = mySkillLevels[CombatSkill.Dodge] - blockMod/2f;
        a.parry = mySkillLevels[CombatSkill.Parry] - blockMod/2f;
        a.will  = mySkillLevels[CombatSkill.Willpower];
        return a;
    }

    private void LoadDefaultSkills()
    {
        for (int i = 0; i < defaultWeapons.Count; i++)
        {
            myWeaponSkillLevels[defaultWeapons[i]] = defaultWeaponStats[i];
        }

        for (int i = 0; i < defaultSkills.Count; i++)
        {
            mySkillLevels[defaultSkills[i]] = defaultSkillStats[i];
        }
    }

    #region Saving and Loading

    //COMBAT SKILLS
    public Dictionary<CombatSkill, int> GetCombatLevels()
    {
        return mySkillLevels;
    }

    public Dictionary<CombatSkill, float> GetCombatExperience()
    {
        return mySkillExperience;
    }

    public void LoadSavedCombatLevels(Dictionary<CombatSkill, int> savedLevels)
    {
        mySkillLevels = savedLevels;
    }

    public void LoadSavedCombatExperience(Dictionary<CombatSkill, float> savedExperience)
    {
        mySkillExperience = savedExperience;
    }


    //WEAPONS

    public Dictionary<Item.WeaponType, int> GetWeaponLevels()
    {
        return myWeaponSkillLevels;
    }

    public Dictionary<Item.WeaponType, float> GetWeaponExperience()
    {
        return myWeaponSkillExperience;
    }

    public void LoadSavedWeaponLevels(Dictionary<Item.WeaponType, int> savedLevels)
    {
        myWeaponSkillLevels = savedLevels;
    }

    public void LoadSavedWeaponExperience(Dictionary<Item.WeaponType, float> savedExperience)
    {
        myWeaponSkillExperience = savedExperience;
    }
    #endregion

    //Player callback for weapon swaps (called from EquipmentManager)
    private void SwapOffHand(Equipment oldItem, Equipment newItem)
    {/*
        Weapon w = (Weapon)equipmentManager.EquipmentFromSlot(EquipmentSlot.OffHand);
        if(w == null)
        {
            equippedOffHand = new WeaponInfo(); //clear the equipped offhand
        }
        else
        {
            equippedOffHand = w.RequestWeaponInfo();
        }
        */
    }

}

public struct AttackInfo
{
    public string attackerName;
    public Item weapon;
    public float force;
    public float skill;
    public float speed;
}

//this is passed to the human body to see if unit can react to the attack without taking damage
public struct AttackReactionSkills
{
    public float dodge, block, parry, will;
}