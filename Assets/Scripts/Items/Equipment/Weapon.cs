using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Inventory/Weapon")]
public class Weapon : Equipment {

    #region Stats
    public enum WeaponType { Dagger, Spear, Axe, Pick, Hands };
    public WeaponType weaponType;

    public enum ToolType { Pick, Axe, NA };
    public ToolType toolType;

    public enum AttackType { Melee, Cast, Ranged };
    public AttackType attackType;

    [SerializeField]
    private float weaponCondition = 1; //1 is undamaged, might remove and just have sharpness degrade...but I also might want broken weapons.
    [SerializeField]
    private float sharpness = 1; //the sharper the better
    [SerializeField]
    private float weight = 1; //the heavier the better
    #endregion

    public float range
    {
		get
        {
			if(attackType != AttackType.Melee)
            {
				return 5f;
			}
            else
            {
				return 1f;
			}
		}
	}
	
	public float speed
    {
		get
        {
            return weight;// / 2; //eventually will change to something like weight - skill + 2 / 1, so it is at least always one second
		}
	}

    public float totalAttack
    {
        get
        {
            Debug.LogWarning("USING OBSOLETE totalAttack");
            return 0;
        }   
    }

    //weapon and armor populate their own stats because they differ between the two
    public override void OpenStatWindow(string itemLocation)
    {
        base.OpenStatWindow(itemLocation);

        EquipmentStats window = EquipmentStats.instance;
        if(window != null)
            window.PopulateStats(PackageWeaponInfo());
    }

    public WeaponInfo RequestWeaponInfo()
    {
        WeaponInfo info = new WeaponInfo();
        info.weaponType = weaponType;
        info.sharpness = sharpness;
        info.hardnessValue = hardnessValue;
        info.weight = weight;
        info.speed = speed;
        info.name = name;
        return info;
    }

    private string[] PackageWeaponInfo()
    {
        string[] myStats = new string[5];
        myStats[0] = name;
        myStats[1] = "Quality: " + hardnessValue.ToString();
        myStats[2] = "Weight: " + weight.ToString();
        myStats[3] = "Value: " + currentValue.ToString();
        myStats[4] = myDescription;

        return myStats;
    }
}

public struct WeaponInfo
{
    public Weapon.WeaponType weaponType;
    public float hardnessValue;
    public float sharpness;
    public float weight;
    public float speed;
    public string name;
}