using UnityEngine.UI;
using UnityEngine;

public class EquipSlot : InventorySlot {

    EquipmentManager equipmentManager;
    Equipment equipment;

    void Start()
    {
        equipmentManager = PlayerManager.instance.player.GetComponent<EquipmentManager>();
    }

    public void AddEquipment(Equipment newItem)
    {
        equipment = newItem;
        icon.sprite = equipment.icon;
        icon.enabled = true;
        removeButton.interactable = true;
    }

    public override void OnRemoveButton()
    {
        equipmentManager.Unequip((int)equipment.equipSlot);
    }

    public override void SlotRightClicked()
    {
        if (equipment != null)
        {
            //display equipment stat window ( Equipment.Use() currently equips items if it is in the inventory but does not unequip )
            equipment.Use();
        }
    }

    public override void SlotLeftClicked()
    {
        if (equipment != null)
        {
            //does nothing right now
            //eventually this can be placed in our mouseSlot and put in the inventory
            //but focus on inventory item movement first
            Debug.Log("Putting equipment into mouseSlot");
        }
    }
}
