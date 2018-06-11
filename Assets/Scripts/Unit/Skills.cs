using System.Collections.Generic;
using UnityEngine;

//MIGHT BE BEST TO RENAME THIS SCRIPT HUMANOID COMBAT SKILLS
//HAVE A SEPERATE SCRIPT FOR OTHER SKILLS THAT WILL CHECK BODYPARTS FOR BASESKILL - REALTED BODYPART HEALTH
public class Skills : MonoBehaviour {
    private HumanoidBody body;

    public float dagger, spear, axe, pick, unarmed;
    public float baseBlock, baseParry, baseDodge, baseHealth;
    public float strength, agility;

    private Dictionary<HumanoidBody.HumanoidBodyParts, float> bodyPartHealth;

    [SerializeField]
    private float block
    {
        get
        {
            float leftArm = body.bodyPartHealth[HumanoidBody.HumanoidBodyParts.LeftArm];
            float rightArm = body.bodyPartHealth[HumanoidBody.HumanoidBodyParts.RightArm];
            float leftHand = body.bodyPartHealth[HumanoidBody.HumanoidBodyParts.LeftHand];
            float rightHand = body.bodyPartHealth[HumanoidBody.HumanoidBodyParts.RightHand];
            return baseBlock * ((leftArm + rightArm + leftHand + rightHand / 4) * 0.01f);
        }
    }

    [SerializeField]
    private float dodge
    {
        get
        {
            float leftLeg = body.bodyPartHealth[HumanoidBody.HumanoidBodyParts.LeftLeg];
            float rightLeg = body.bodyPartHealth[HumanoidBody.HumanoidBodyParts.RightLeg];
            float leftFoot = body.bodyPartHealth[HumanoidBody.HumanoidBodyParts.LeftFoot];
            float rightFoot = body.bodyPartHealth[HumanoidBody.HumanoidBodyParts.RightFoot];
            return baseDodge * ((leftLeg + rightLeg + leftFoot + rightFoot / 4) * 0.01f);
        }
    }

    [SerializeField]
    private float parry
    {
        get
        {
            float leftArm = body.bodyPartHealth[HumanoidBody.HumanoidBodyParts.LeftArm];
            float rightArm = body.bodyPartHealth[HumanoidBody.HumanoidBodyParts.RightArm];
            float leftHand = body.bodyPartHealth[HumanoidBody.HumanoidBodyParts.LeftHand];
            float rightHand = body.bodyPartHealth[HumanoidBody.HumanoidBodyParts.RightHand];
            float leftLeg = body.bodyPartHealth[HumanoidBody.HumanoidBodyParts.LeftLeg];
            float rightLeg = body.bodyPartHealth[HumanoidBody.HumanoidBodyParts.RightLeg];
            float leftFoot = body.bodyPartHealth[HumanoidBody.HumanoidBodyParts.LeftFoot];
            float rightFoot = body.bodyPartHealth[HumanoidBody.HumanoidBodyParts.RightFoot];
            return baseParry * ((leftArm + rightArm + leftHand + rightHand + leftLeg + rightLeg + leftFoot + rightFoot / 8) * 0.01f);
        }
    }

    //might be cleaner to just use overall health for most things?
    private float overallHealth
    {
        get
        {
            float leftArm = body.bodyPartHealth[HumanoidBody.HumanoidBodyParts.LeftArm];
            float rightArm = body.bodyPartHealth[HumanoidBody.HumanoidBodyParts.RightArm];
            float leftHand = body.bodyPartHealth[HumanoidBody.HumanoidBodyParts.LeftHand];
            float rightHand = body.bodyPartHealth[HumanoidBody.HumanoidBodyParts.RightHand];
            float leftLeg = body.bodyPartHealth[HumanoidBody.HumanoidBodyParts.LeftLeg];
            float rightLeg = body.bodyPartHealth[HumanoidBody.HumanoidBodyParts.RightLeg];
            float leftFoot = body.bodyPartHealth[HumanoidBody.HumanoidBodyParts.LeftFoot];
            float rightFoot = body.bodyPartHealth[HumanoidBody.HumanoidBodyParts.RightFoot];
            float head = body.bodyPartHealth[HumanoidBody.HumanoidBodyParts.Head];
            float neck = body.bodyPartHealth[HumanoidBody.HumanoidBodyParts.Neck];
            float chest = body.bodyPartHealth[HumanoidBody.HumanoidBodyParts.Chest];
            float abdomin = body.bodyPartHealth[HumanoidBody.HumanoidBodyParts.Abdomin];

            return baseHealth * ((leftArm + rightArm + leftHand + rightHand + leftLeg + rightLeg + leftFoot + rightFoot +
                                     abdomin + chest + neck + head / 12) * 0.01f);
        }
    }

    private void Start()
    {
        body = GetComponent<HumanoidBody>();
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

    //copies defense skills and gives it to the body DefenseSkills is not public so we use a different struct
    public AttackReactionSkills GetAttackReactionSkills()
    {
        AttackReactionSkills a = new AttackReactionSkills();
        a.block = block;
        a.dodge = dodge;
        a.parry = parry;

        return a;
    }

    private float WeaponSkill(Weapon.WeaponType weaponType)
    {
        float skill;

        switch (weaponType)
        {
            case Weapon.WeaponType.Dagger:
                skill = overallHealth * dagger;
                break;
            case Weapon.WeaponType.Axe:
                skill = overallHealth * axe;
                break;
            case Weapon.WeaponType.Spear:
                skill = overallHealth * spear;
                break;
            case Weapon.WeaponType.Pick:
                skill = overallHealth * pick;
                break;
            case Weapon.WeaponType.Hands:
                skill = overallHealth * pick;
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

//this is passed to the human body to see if unit can react to the attack without taking damage
public struct AttackReactionSkills
{
    public float dodge, block, parry;
}


