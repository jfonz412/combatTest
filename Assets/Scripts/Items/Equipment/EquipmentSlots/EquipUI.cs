using UnityEngine;

public class EquipUI : MonoBehaviour {

    public Transform equipParent;
    public GameObject equipmentUI;
    EquipmentManager equipmentManager;
    EquipSlot[] slots;


    // Use this for initialization
    void Start () {
        equipmentManager = PlayerManager.instance.player.GetComponent<EquipmentManager>();
        equipmentManager.onEquipmentChanged += UpdateUI;
        slots = equipParent.GetComponentsInChildren<EquipSlot>();
        AssignSlotNums();
    }
	
	// Update is called once per frame
	void Update () {
        InventoryToggle();
    }

    void UpdateUI(Equipment oldItem, Equipment newItem)
    {
        if (newItem != null)
        { 
            int slotNum = (int)newItem.equipSlot;
            slots[slotNum].AddEquipment(equipmentManager.currentEquipment[slotNum]);
        }
        else
        {
            int slotNum = (int)oldItem.equipSlot;
            slots[slotNum].ClearSlot();
        }
    }

    void InventoryToggle()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            equipmentUI.SetActive(!equipmentUI.activeSelf);
        }

        if (DialogueManager.instance.isOpen)
        {
            equipmentUI.SetActive(false);
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
