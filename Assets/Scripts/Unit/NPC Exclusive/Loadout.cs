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
            loadout[i] = Instantiate(loadout[i]);
            loadout[i].Init(); 

            equipmentManager.Equip(loadout[i]);
            Debug.Log(gameObject.name + " instantiated " + loadout[i] + " equipment from LoadOut");
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

    //for player only, this should eventually apply equipment conditions (sharpness, condition, hardness value...)
    //need to tie this info to the item somehow and apply it after it's been instantiated
    //maybe armor and weapons can take this info through Init(), but I need to make sure the correct info goes to the correct equipment somehow
    //refrence PlayerData's SavedItem[] and how it loads inventory for this. Might make sense to split player Loadout off from this
    private Equipment[] GetEquipmentFromNames(string[] equipmentNames)
    {
        Equipment[] equipment = new Equipment[6];

        for (int i = 0; i < equipmentNames.Length; i++)
        {
            equipment[i] = (Equipment)Resources.Load(equipmentNames[i]);
        }

        return equipment;
    }
}
