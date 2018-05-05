using UnityEngine;

public class Loadout : MonoBehaviour {
    public Equipment[] defaultLoadout = new Equipment[6]; //num of equipslots 
    private EquipmentManager equipmentManager;

    void Start()
    {
        equipmentManager = GetComponent<EquipmentManager>();
        LoadDefaultEquipment();
    }

    void LoadDefaultEquipment()
    {
        for (int i = 0; i < defaultLoadout.Length; i++)
        {
            equipmentManager.Equip(defaultLoadout[i]);
        }
    }
}
