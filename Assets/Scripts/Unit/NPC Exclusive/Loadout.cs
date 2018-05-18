using UnityEngine;

public class Loadout : MonoBehaviour {
    [SerializeField]
    private Equipment[] defaultLoadout = new Equipment[6]; //num of equipslots 
    private EquipmentManager equipmentManager;

    private void Start()
    {
        equipmentManager = GetComponent<EquipmentManager>();
        LoadDefaultEquipment();
    }

    // will be called when the scene is loaded
    public void LoadEquipment(int[] equipmentIDs = null)
    {
        equipmentIDs = null; //REMOVE THIS LINE

        if (equipmentIDs == null)
        {
            LoadDefaultEquipment();
        }
        else
        {
            Equipment[] equipment = GetEquipmentFromIDs(equipmentIDs);

            for (int i = 0; i < defaultLoadout.Length; i++)
            {
                equipmentManager.Equip(equipment[i]);
            }
        }
    }

    private Equipment[] GetEquipmentFromIDs(int[] equipmentIDs)
    {
        Equipment[] equipment = new Equipment[6];

        for (int i = 0; i < equipmentIDs.Length; i++)
        {
            //get equipment by using ID using EquipmentList
        }

        return equipment;
    }

    private void LoadDefaultEquipment()
    {
        for (int i = 0; i < defaultLoadout.Length; i++)
        {
            equipmentManager.Equip(defaultLoadout[i]);
        }
    }


}
