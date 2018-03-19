using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Button removeButton;
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
        icon.rectTransform.sizeDelta = new Vector2(50f, 50f); //temporary, the long term solution would be to make a custom icon sprite for each item
        icon.enabled = true;
        removeButton.interactable = true;
    }

    public virtual void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }

    public virtual void OnRemoveButton()
    {
        Inventory.instance.RemoveAndDestroy(item);
    }

    public virtual void SlotHoverOver()
    {
        SlotClick.InventorySlotHoverOver(item);
    }

    public virtual void SlotRightClicked()
    {
        SlotClick.InventorySlotRightClicked(item);
    }

    public virtual void SlotLeftClicked()
    {
        if(PlayerState.currentState == PlayerState.PlayerStates.Shopping)
        {
            SlotClick.LeftClickedToSell(this);
        }
        else
        {
            SlotClick.InventorySlotLeftClicked(this);
        }

    }
}
