using UnityEngine;

[CreateAssetMenu(fileName = "New Armor", menuName = "Inventory/Armor")]
public class Armor : Equipment {
    [SerializeField]
    public float armorCondition = 1f; //1 = no damage to armor 
    [SerializeField]
    public float protection = 1f; //essentially baseDefense
    [SerializeField]
    public float weight = 0f; //used to determine if unit is strong enough to equip

	public float defense{
		get {
			return protection * armorCondition;
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

    string[] PackageArmorInfo()
    {
        string[] myStats = new string[5];
        myStats[0] = name;
        myStats[1] = "Condition: " + armorCondition.ToString();
        myStats[2] = "Weight: " + weight.ToString();
        myStats[3] = "Value: " + currentValue.ToString();
        myStats[4] = myDescription;

        return myStats;
    }

}
