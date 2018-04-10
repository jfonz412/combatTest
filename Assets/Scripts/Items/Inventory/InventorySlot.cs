using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Item item;
    [HideInInspector]
    public int slotNum;


    public virtual void AddItem(Item newItem)
    {
        if (newItem == null)
        {
            ClearSlot();
            return;
        }

        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
    }

    public virtual void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
    }


    public virtual void SlotHoverOver()
    {
        SlotClick.instance.InventorySlotHoverOver(item);
    }

    public virtual void SlotRightClicked()
    {
        SlotClick.instance.InventorySlotRightClicked(item);

        if (PlayerState.currentState == PlayerState.PlayerStates.Shopping)
        {
            SlotClick.instance.RightClickedToSell(item);
        }
        else
        {
            SlotClick.instance.InventorySlotRightClicked(item);
        }
    }

    public virtual void SlotLeftClicked()
    {
        if(PlayerState.currentState == PlayerState.PlayerStates.Shopping)
        {
            SlotClick.instance.LeftClickedToSell(this);
        }
        else
        {
            SlotClick.instance.InventorySlotLeftClicked(this);
        }

    }
}
