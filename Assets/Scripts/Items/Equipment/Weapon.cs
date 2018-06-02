using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Inventory/Weapon")]
public class Weapon : Equipment {

    #region Stats
    public enum ToolType { Pick, Axe, NA };
    public ToolType toolType;

    public enum AttackType { Melee, Cast, Ranged };
    public AttackType attackType;

    [SerializeField]
    private float weaponCondition = 1; //1 is undamaged
    [SerializeField]
    private float softness = 1; //1 is max hardness, the harder the better (should be hardness I think, makes more sense)
    [SerializeField]
    private float sharpness = 1; //the sharper the better
    [SerializeField]
    private float weight = 1; //the heavier the better
    #endregion

    public float range{
		get{
			if(attackType != AttackType.Melee)
            {
				return 5f;
			}else{
				return 1f;
			}
		}
	}
	
	public float speed{
		get{
			return weight / 2;
		}
	}

    public float totalAttack
    {
        get
        {
            return weight + sharpness * softness * weaponCondition;
        }   
    }

    //weapon and armor populate their own stats because they differ between the two
    public override void OpenStatWindow(string itemLocation)
    {
        base.OpenStatWindow(itemLocation);

        EquipmentStats window = EquipmentStats.instance;
        if(window != null)
            window.PopulateStats(PackageMyInfo());
    }

    private string[] PackageMyInfo()
    {
        string[] myStats = new string[5];
        myStats[0] = name;
        myStats[1] = "Condition: " + weaponCondition.ToString();
        myStats[2] = "Weight: " + weight.ToString();
        myStats[3] = "Value: " + currentValue.ToString();
        myStats[4] = myDescription;

        return myStats;
    }


    public override string GetResourcePath()
    {
        string directory;

        if (toolType == ToolType.NA)
        {
            directory = "Items/Weapons/";
        }
        else
        {
            directory = "Items/Tools/";
        }
            

        return directory + myFileName;
    }
}
