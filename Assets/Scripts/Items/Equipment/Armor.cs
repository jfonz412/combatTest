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

        string[] myStats = new string[4];
        myStats[0] = "Defense: " + defense.ToString();
        myStats[1] = "Condition: " + armorCondition.ToString();
        myStats[2] = "Weight: " + weight.ToString();
        myStats[3] = "Value: " + currentValue.ToString();

        EquipmentStats.instance.PopulateStats(myStats);
    }
}
