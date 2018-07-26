using UnityEngine;

public class EquipSlot : InventorySlot {

    //SlotClick.instance.cs needs to know about these
    [HideInInspector]
    public EquipmentManager equipmentManager;
    [HideInInspector]
    public Item equipment;

    public Item.EquipmentSlot slotType;

    void Start()
    {
        equipmentManager = ScriptToolbox.GetInstance().GetPlayerManager().player.GetComponent<EquipmentManager>();
    }

    public void AddEquipment(Item newItem)
    {
        if (newItem == null)
        {
            ClearSlot();
            return;
        }

        equipment = newItem;
        //icon.sprite = equipment.icon;
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

