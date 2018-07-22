using UnityEngine;

[CreateAssetMenu(fileName = "New Armor", menuName = "Inventory/Armor")]
public class Armor : Equipment
{
    /*
    [SerializeField]
    public float armorCondition = 1f; //1 = no damage to armor 
    [SerializeField]
    public float weight = 0f; //used to determine if unit is strong enough to equip

	public float defense{
		get {
            Debug.LogWarning("USING OBSOLETE totalAttack");
            return 0;
        }
	}

    //weapon and armor populate their own stats because they differ between the two
    public override void OpenStatWindow(string itemLocation)
    {
        base.OpenStatWindow(itemLocation);

        EquipmentStats window = EquipmentStats.instance;
        if (window != null)
            window.PopulateStats(PackageArmorInfo());
    }

    private string[] PackageArmorInfo()
    {
        string[] myStats = new string[5];
        myStats[0] = name;
        myStats[1] = "Quality: " + hardnessValue.ToString();
        myStats[2] = "Weight: " + weight.ToString();
        myStats[3] = "Value: " + currentValue.ToString();
        myStats[4] = myDescription;

        return myStats;
    }

    //skip over naming if not using weapon or amrmor, helps with HumanInjuries.DamageMessage()
    public ArmorInfo GetArmorInfo()
    {
        ArmorInfo info = new ArmorInfo();

        info.name = name;
        info.protectionValue = hardnessValue;
        info.armorType = equipSlot;
        return info;
    }
}

public struct ArmorInfo
{
    public string name;
    public EquipmentSlot armorType;
    public float protectionValue;
}
*/
}
