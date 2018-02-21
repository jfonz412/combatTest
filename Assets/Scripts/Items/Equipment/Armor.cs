using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "New Armor", menuName = "Inventory/Armor")]
public class Armor : Equipment {	
	public float armorCondition = 1; //1 = no damage to armor 
	public float protection = 1; //essentially baseDefense
	public float weight = 0; //used to determine if unit is strong enough to equip
	
	public float defense{
		get {
			return protection * armorCondition;
		}
	}
}
