using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Inventory/Weapon")]
public class Weapon : Equipment {

    #region Stats
    //these are only public so they can be editted in the inspector
    public float weaponCondition = 1; //1 is undamaged
	public float softness = 1; //1 is max hardness, the harder the better (should be hardness I think, makes more sense)
	public float sharpness = 1; //the sharper the better
	public float weight = 1; //the heavier the better

	public string attackType; // cast, slash, thrust
    #endregion

    public float range{
		get{
			if(attackType == "cast" || attackType == "ranged"){
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

        string[] myStats = new string[4];
        myStats[0] = "Attack: " + totalAttack.ToString();
        myStats[1] = "Condition: " + weaponCondition.ToString();
        myStats[2] = "Weight: " + weight.ToString();
        myStats[3] = "Value: " + currentValue.ToString();

        EquipmentStats.instance.PopulateStats(myStats);
    }
}
