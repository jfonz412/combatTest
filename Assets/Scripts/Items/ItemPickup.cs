using UnityEngine;

public class ItemPickup : Interactable {

    public MasterItemList.Items baseItem;
    public int myQuantity;
    public Item item;

    void Start()
    {
        myInteractions = new string[] { "Pickup", "Inspect", "--", "--" };
        item = MasterItemList.GetItem(baseItem); 

        //optional quantity setting, will be 0 if not manually set
        if(myQuantity > 0)
        {
            Debug.Log("Setting item quantity to " + myQuantity);
            if(myQuantity <= item.maxQuantity)
                item.quantity = (int)myQuantity;
        }
        Debug.Log("now my quantity is " + item.quantity);
    }

    public override void Interaction(string interaction)
    {
        base.Interaction(interaction); //gets the reference to the player

        if (interaction == "Default")
        {
            interaction = defaultInteraction.ToString();
        }

        //chose which interaction to trigger
        switch (interaction)
        {
            case "Pickup":
                PickUp();
                break;
            case "Inspect":
                InspectObject(); 
                break;
            default:
                Debug.Log("invalid " + interaction + " interaction passed to " + gameObject);
                break;
        }
    }

    void PickUp()
    {
        Debug.Log("Picking up " + item.name + " with quantity of " + item.quantity);
        int itemsLeftAfterPickup = InventoryManager.GetInstance().GetInventory().AddItem(item);

        if (itemsLeftAfterPickup == 0)
        {
            item = null;
            Destroy(transform.gameObject);
        }
        else
        {
            item.quantity = itemsLeftAfterPickup;
        }
    }
}
