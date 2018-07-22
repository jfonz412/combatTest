using UnityEngine;

public class Loadout : MonoBehaviour {
    [SerializeField]
    private Item[] loadout = new Item[7]; //num of equipslots 

    //called on load for NPCs, called if no save data is found for player to load default equipment
    public void EquipLoadout()
    {
        /*
        Debug.Log("Equipping loadout for " + gameObject.name);

        EquipmentManager equipmentManager = GetComponent<EquipmentManager>();

        for (int i = 0; i < loadout.Length; i++)
        {
            loadout[i] = Instantiate(loadout[i]);
            Debug.Log("Need to use Item Master List here");
            //loadout[i].Init(); 

            equipmentManager.Equip(loadout[i]);
        }
            */
        Debug.Log("This is being called but it's not doing anything, script is obsolete");
    }
}
