using UnityEngine;

public class EquipSlot : InventorySlot {

    //SlotClick.instance.cs needs to know about these
    [HideInInspector]
    public EquipmentManager equipmentManager;
    [HideInInspector]
    public Equipment equipment;

    public int equipSlot;

    void Start()
    {
        equipmentManager = PlayerManager.instance.player.GetComponent<EquipmentManager>();
    }

    public void AddEquipment(Equipment newItem)
    {
        if (newItem == null)
        {
            ClearSlot();
            return;
        }

        equipment = newItem;
        icon.sprite = equipment.icon;
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
        EquipSlotClick.instance.EquipSlotHoverOver(this);
    }

    public override void SlotRightClicked()
    {
        EquipSlotClick.instance.EquipSlotRightClicked(this);
    }

    public override void SlotLeftClicked()
    {
        EquipSlotClick.instance.EquipSlotLeftClicked(this);
    }
}

