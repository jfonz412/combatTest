﻿using UnityEngine;

public class ItemPickup : Interactable {
    public Item item;

    public override void DefaultInteraction()
    {
        base.DefaultInteraction();
        PickUp();    
    }

    void PickUp()
    {
        Debug.Log("Picking up " + item.name);
        if (Inventory.instance.Add(item) == true)
        {
            Destroy(gameObject);
        }
    }
}
