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

    public virtual void SlotRightClicked()
    {
        if (item != null)
        {
            item.Use();
        }
    }

    public virtual void SlotLeftClicked()
    {
        Inventory inventory = Inventory.instance;
        MouseSlot mouseSlot = MouseSlot.instance;
        
        Item mouseItem = MouseSlot.instance.currentItem; //save a copy of the mouseItem
                               
        
        //if both slots are null, exit immediatly
        if(mouseItem == null && item == null)
        {
            Debug.Log("BOTH SLOTS EMPTY");
            return;
        }

        //just pick up item out of slot
        if(mouseItem == null && item != null)
        {
            Debug.Log("PICK UP ITEM INTO EMPTY MOUSE SLOT");
            Item previousItem = item;             //save a copy of the slotItem
            inventory.Remove(previousItem);       //remove the item in the slot 
            mouseSlot.UpdateItem(previousItem); //place previous item in the mouseSlot

            return;
        }

        //place mouse item in empty slot
        if(mouseItem != null && item == null)
        {
            Debug.Log("PLACING ITEM IN EMPTY SLOT");
            mouseItem.slotNum = slotNum; //assign item's slotNum to this slot
            inventory.AddToSpecificSlot(mouseItem); //drop item in slot
            mouseSlot.UpdateItem(null); //clear mouseSlot's item

            return;
        }
        //AFTER THE ABOVE IT CALLED THE BELOW IS NOW TRUE!
        if(mouseItem != null && item != null)
        {
            Debug.Log("SWAPPING ITEMS");
            Item previousItem = item;                 //save a copy of the slotItem
            mouseItem.slotNum = slotNum;              //assign item's slotNum to this slot
            inventory.AddToSpecificSlot(mouseItem);   //drop item in slot, removing old item is taken care of here too
            mouseSlot.UpdateItem(previousItem);     //add old item to mouseSlot

            return;
        }
    }

    /*
             if(item == null && mouseItem == null)
        {
            return;
        }

        if (mouseItem == null)
        {
            inventory.Remove(item);               //clear the slot
            mouseSlot.currentItem = previousItem; //add inventory item to mouse slot 
        }
        else if(mouseItem != null)
        {

            mouseItem.slotNum = slotNum; //change the slot number before adding

            inventory.AddToSpecificSlot(mouseItem);

            if (previousItem != null)
            {
                inventory.Remove(item);
            }

            mouseSlot.currentItem = previousItem; //add old inventory item to mouse slot 


        }     
     */

}
