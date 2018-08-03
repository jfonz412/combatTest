using UnityEngine;

public class EquipSlot : InventorySlot {

    private EquipmentManager equipmentManager;
    protected Item equipment;

    public Item.EquipmentSlot slotType;

    public EquipmentManager EquipmentManager()
    {
        if(equipmentManager == null)
            equipmentManager = ScriptToolbox.GetInstance().GetPlayerManager().player.GetComponent<EquipmentManager>();
        return equipmentManager;
    }

    public Item Equipment()
    {
        return equipment;
    }

    public void AddEquipment(Item newItem)
    {
        if (newItem == null)
        {
            ClearSlot();
            return;
        }

        equipment = newItem;

        Sprite itemSprite = Resources.Load<Sprite>("Images/ItemIcons/" + equipment.icon);
        icon.sprite = itemSprite;
        icon.enabled = true;
    }

    public override void ClearSlot()
    {
        equipment = null;
        icon.sprite = null;
        icon.enabled = false;
    }

    public override void SlotHoverOver()
    {
        InventoryManager.GetInstance().GetEquipSlotClick().EquipSlotHoverOver(this);
    }

    public override void SlotRightClicked()
    {
        InventoryManager.GetInstance().GetEquipSlotClick().EquipSlotRightClicked(this);
    }

    public override void SlotLeftClicked()
    {
        InventoryManager.GetInstance().GetEquipSlotClick().EquipSlotLeftClicked(this);
    }
}

