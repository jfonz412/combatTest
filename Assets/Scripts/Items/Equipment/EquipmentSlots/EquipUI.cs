using UnityEngine;

public class EquipUI : MonoBehaviour {

    public Transform equipParent;
    public GameObject equipmentUI;
    EquipmentManager equipmentManager;
    EquipSlot[] slots;

    PlayerState.PlayerStates[] invalidStates = new PlayerState.PlayerStates[]
    {
        PlayerState.PlayerStates.Dead,
        PlayerState.PlayerStates.Speaking
    };

    void Start()
    {
        equipmentManager = PlayerManager.instance.player.GetComponent<EquipmentManager>();
        equipmentManager.onEquipmentChanged += UpdateUI;
        slots = equipParent.GetComponentsInChildren<EquipSlot>();
        AssignSlotNums();
    }
	

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

        CheckForValidPlayerState();
    }

    void CheckForValidPlayerState()
    {
        if (PlayerState.CheckPlayerState(invalidStates))
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
