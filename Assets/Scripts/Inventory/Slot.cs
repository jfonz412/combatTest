using UnityEngine.UI;
using UnityEngine;
public class Slot : MonoBehaviour {

    public Image icon;
    public Button removeButton;
    public Item item;

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
        if (item != null)
        {
            Inventory.instance.Remove(item);
        }
    }
}
