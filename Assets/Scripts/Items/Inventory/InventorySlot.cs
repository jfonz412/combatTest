using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Button removeButton;
    public Item item;

    public int slotNum;


    public void AddItem(Item newItem)
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


    #region Slot Clicks
    public virtual void SlotRightClicked()
    {
        if (item != null)
        {
            item.OpenStatWindow();
        }
    }
  
    public virtual void SlotLeftClicked()
    {
        MouseSlot mouseSlot = MouseSlot.instance;      
        Item mouseItem = MouseSlot.instance.currentItem; 
                               
        if(mouseItem == null && item == null)
        {
            Debug.Log("BOTH SLOTS EMPTY");
            return;
        }

        if(mouseItem == null && item != null)
        {
            PickUpItemIntoEmptyMouseSlot(mouseSlot);
            return;
        }

        if(mouseItem != null && item == null)
        {
            PlaceItemInEmptySlot(mouseSlot);
            return;
        }

        if(mouseItem != null && item != null)
        {
            SwapItems(mouseSlot);
            return;
        }
    }
    #endregion

    #region Click Helpers

    void PickUpItemIntoEmptyMouseSlot(MouseSlot mouseSlot)
    {
        Inventory inventory = Inventory.instance;

        Debug.Log("PICK UP ITEM INTO EMPTY MOUSE SLOT");
        Item previousItem = item;             //save a copy of the slotItem
        inventory.Remove(previousItem);       //remove the item in the slot 
        mouseSlot.UpdateItem(previousItem); //place previous item in the mouseSlot            //place previous item in the mouseSlot (as an item)?
    }

    void PlaceItemInEmptySlot(MouseSlot mouseSlot)
    {
        Inventory inventory = Inventory.instance;
        Item mouseItem = mouseSlot.currentItem;

        Debug.Log("PLACING ITEM IN EMPTY SLOT");
        mouseItem.slotNum = slotNum; //assign item's slotNum to this slot
        inventory.AddToSpecificSlot(mouseItem); //drop item in slot
        mouseSlot.UpdateItem(null); //clear mouseSlot's item
    }

    void SwapItems(MouseSlot mouseSlot)
    {
        Inventory inventory = Inventory.instance;
        Item mouseItem = mouseSlot.currentItem;
        Item previousItem = item;

        Debug.Log("SWAPPING ITEMS");
        mouseItem.slotNum = slotNum;              //assign item's slotNum to this slot
        inventory.AddToSpecificSlot(mouseItem);   //drop item in slot, removing old item is taken care of here too
        mouseSlot.UpdateItem(previousItem);     //add old item to mouseSlot
    }
    #endregion
}
