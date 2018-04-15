﻿using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")] //allows us to create Items like we can create folders, scripts, etc.
public class Item : ScriptableObject {

    public Sprite icon = null;
    public int? slotNum; //allows the int to be null

    new public string name = "New Item";

    public float baseValue = 100f;
    public float currentValue;

    public int quantity = 1;
    public int maxQuantity = 99;
    public bool stackable;

    //create seperate item classes (equipment, consumable, crafting, etc. to handle differernt use cases)
    public virtual void Use()
    {
        Transform player = PlayerManager.instance.player.transform;

        Inventory.instance.CondenseStackables(this, 1);
        Debug.Log("Using " + name);
    }

    public virtual void OpenStatWindow(string itemLocation)
    {
        DetermineValue(itemLocation);
        Instantiate(Resources.Load("ItemMenu"), InfoPanel.instance.transform.position, Quaternion.identity, InfoPanel.instance.transform);

        ItemMenu.instance.PopulateStats(this);
    }

    void RemoveFromInventory()
    {
        Inventory.instance.RemoveAndDestroy(this);
    }

    public void DetermineValue(string itemLocation)
    {
        if (itemLocation == "Shop")
        {
            currentValue = PriceChecker.AppraiseItem(this, "Purchase");
        }
        else
        {
            currentValue = PriceChecker.AppraiseItem(this, "Sale");
        }
    }
}
