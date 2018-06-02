using UnityEngine;

public class Loadout : MonoBehaviour {
    [SerializeField]
    private Equipment[] loadout = new Equipment[6]; //num of equipslots 

    //called on load for NPCs, called after EquipUI delegate assigned for player
    public void EquipLoadout()
    {
        EquipmentManager equipmentManager = GetComponent<EquipmentManager>();

        for (int i = 0; i < loadout.Length; i++)
        {
            equipmentManager.Equip(loadout[i]);
        }
    }

    public void LoadSavedEquipment(string[] equipmentNames = null)
    {
        if (equipmentNames == null)
        {
            return;
        }
        else 
        {
            loadout = GetEquipmentFromNames(equipmentNames);
        }
    }

    private Equipment[] GetEquipmentFromNames(string[] equipmentNames)
    {
        Equipment[] equipment = new Equipment[6];

        for (int i = 0; i < equipmentNames.Length; i++)
        {
            //Debug.Log(equipmentNames[i]);
            Debug.Log(equipmentNames[i]);
            equipment[i] = (Equipment)Instantiate(Resources.Load(equipmentNames[i]));
            //Debug.Log(equipment[i]);
        }

        return equipment;
    }
}
