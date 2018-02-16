﻿using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
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

    public void UseItem()
    { 
        if (item != null)
        {
            item.Use();
        }
    }

    public void SwapWithMouseSlot()
    {
        if (item != null)
        {
            Debug.Log("Putting item in mouse slot");
            Inventory.instance.Remove(item);
        }
    }
}
