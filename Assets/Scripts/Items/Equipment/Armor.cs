using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "New Armor", menuName = "Inventory/Armor")]
public class Armor : Equipment {	
	public float armorCondition = 1f; //1 = no damage to armor 
	public float protection = 1f; //essentially baseDefense
	public float weight = 0f; //used to determine if unit is strong enough to equip
    public float value = 100f;

	public float defense{
		get {
			return protection * armorCondition;
		}
	}

    public override void OpenStatWindow()
    {
        base.OpenStatWindow();

        string[] myStats = new string[4];
        myStats[0] = "Defense: " + defense.ToString();
        myStats[1] = "Condition: " + armorCondition.ToString();
        myStats[2] = "Weight: " + weight.ToString();
        myStats[3] = "Value " + value.ToString();

        EquipmentStats.instance.PopulateStats(myStats);
    }
}
