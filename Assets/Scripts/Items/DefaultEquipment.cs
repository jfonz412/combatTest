using UnityEngine;

//all loadouts will inherit from this script
public class DefaultEquipment : MonoBehaviour {
    protected BodyPartController myBody;
    protected Item[] defaultArmor;
    protected Item[] defaultWeapon;

    // Use this for initialization
    void Start () {
        myBody = GetComponent<BodyPartController>();

        InstantiateDefaultEquipment();
        LoadDefaultArmor();
        LoadDefaultWeapon();
	}
	
    protected void InstantiateDefaultEquipment()
    {
        //set default weapons and armor
    }

	// Update is called once per frame
	void LoadDefaultArmor()
    {
        for (int i = 0; i < defaultArmor.Length; i++)
        {
            myBody.EquipArmor(defaultArmor[i]);
        }
    }

    void LoadDefaultWeapon()
    {
        for (int i = 0; i < defaultWeapon.Length; i++)
        {
            myBody.EquipWeapon(defaultWeapon[i]);
        }
    }
}
