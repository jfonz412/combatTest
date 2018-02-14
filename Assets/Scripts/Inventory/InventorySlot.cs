using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {

    public Image icon;
    public Button removeButton;
    Item item;

    //this is called in InventoryUI.UpdateUI which is a callback whenever an item is added or removed
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

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }

    public void OnRemoveButton()
    {
        Inventory.instance.RemoveAndDestroy(item);    
    }

    public void UseItem() //should probably pass in the player's transform here
    { 
        if (item != null)
        {
            item.Use();
        }
    }
}
