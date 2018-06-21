using UnityEngine;

public class Loadout : MonoBehaviour {
    [SerializeField]
    private Equipment[] loadout = new Equipment[7]; //num of equipslots 

    //called on load for NPCs, called if no save data is found for player to load default equipment
    public void EquipLoadout()
    {
        Debug.Log("Equipping loadout for " + gameObject.name);

        EquipmentManager equipmentManager = GetComponent<EquipmentManager>();

        for (int i = 0; i < loadout.Length; i++)
        {
            loadout[i] = Instantiate(loadout[i]);
            loadout[i].Init(); 

            equipmentManager.Equip(loadout[i]);
        }
    }
}
