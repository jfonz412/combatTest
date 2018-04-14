using UnityEngine;

[CreateAssetMenu(fileName = "New Armor", menuName = "Inventory/Armor")]
public class Armor : Equipment {	
	public float armorCondition = 1f; //1 = no damage to armor 
	public float protection = 1f; //essentially baseDefense
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

        string[] myStats = new string[5];
        myStats[0] = name;
        myStats[1] = "Condition: " + armorCondition.ToString();
        myStats[2] = "Weight: " + weight.ToString();
        myStats[3] = "Value: " + currentValue.ToString();
        myStats[4] = "This is a hardcoded description for the item. It will be replaced with a custom string, but for now I want something as long as a typical item description might be.";

        EquipmentStats.instance.PopulateStats(myStats);
    }
}
