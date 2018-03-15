using UnityEngine;

public class EquipSlot : InventorySlot {

    //SlotClick.cs needs to know about these
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
        icon.rectTransform.sizeDelta = new Vector2(50f, 50f); //temporary, the long term solution would be to make a custom icon sprite for each item
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
        SlotClick.EquipSlotHoverOver(this);
    }

    public override void SlotRightClicked()
    {
        SlotClick.EquipSlotRightClicked(this);
    }

    public override void SlotLeftClicked()
    {
        SlotClick.EquipSlotLeftClicked(this);
    }
}

