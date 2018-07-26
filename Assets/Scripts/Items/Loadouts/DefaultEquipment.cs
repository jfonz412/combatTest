using UnityEngine;

//all loadouts will inherit from this script
public class DefaultEquipment : MonoBehaviour {
    protected Item[] defaultArmor;
    protected Item[] defaultWeapon;
    //bools for toggling items on or off would make this script a little more flexable

    // Use this for initialization
    public void EquipLoadout (BodyPartController myBody) {

        InstantiateDefaultEquipment();
        LoadDefaultArmor(myBody);
        LoadDefaultWeapon(myBody);
	}
	
    protected virtual void InstantiateDefaultEquipment()
    {
        //set default weapons and armor
    }

	// Update is called once per frame
	void LoadDefaultArmor(BodyPartController myBody)
    {
        for (int i = 0; i < defaultArmor.Length; i++)
        {
            myBody.EquipArmor(defaultArmor[i]);
        }
    }

    void LoadDefaultWeapon(BodyPartController myBody)
    {
        for (int i = 0; i < defaultWeapon.Length; i++)
        {
            myBody.EquipWeapon(defaultWeapon[i]);
        }
    }
}
