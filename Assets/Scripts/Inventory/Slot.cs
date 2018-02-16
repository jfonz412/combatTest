using UnityEngine.UI;
using UnityEngine;
public class Slot : MonoBehaviour {

    public Image icon;
    public Button removeButton;
    public Item item;

    //this is called in InventoryUI.UpdateUI which is a callback whenever an item is added or removed
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

    public virtual void UseItem()
    {
        if (item != null)
        {
            item.Use();
        }
    }

    public virtual void SwapWithMouseSlot()
    {
        if (item != null)
        {
            Debug.Log("Putting item in mouse slot");
            Inventory.instance.Remove(item);
        }
    }
}
