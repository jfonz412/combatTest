using UnityEngine;

public class EquipUI : MonoBehaviour {

    public Transform equipParent;
    private EquipmentManager equipmentManager;
    private EquipSlot[] slots;

    void Start()
    {
        equipmentManager = ScriptToolbox.GetInstance().GetPlayerManager().player.transform.GetComponent<EquipmentManager>();
        equipmentManager.onEquipmentChanged += UpdateUI;

        slots = equipParent.GetComponentsInChildren<EquipSlot>();
        AssignSlotNums();

        ScriptToolbox.GetInstance().GetPlayerManager().player.GetComponent<Loadout>().EquipLoadout(); //must be called after above delegate
    }

    void UpdateUI(Equipment oldItem, Equipment newItem)
    {
        //Debug.Log("Updating " + gameObject + " with " + newItem);
        if (newItem != null)
        { 
            int slotNum = (int)newItem.equipSlot;
            slots[slotNum].AddEquipment(equipmentManager.EquipmentFromSlot(slotNum));
        }
        else
        {
            int slotNum = (int)oldItem.equipSlot;
            slots[slotNum].ClearSlot();
        }
    }

    void AssignSlotNums()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].equipSlot = i;
        }
    }
}
