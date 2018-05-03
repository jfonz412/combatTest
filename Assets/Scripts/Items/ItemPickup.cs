using UnityEngine;

public class ItemPickup : Interactable {

    public Item baseItem;
    [HideInInspector]
    public Item item;

    void Start()
    {
        myInteractions = new string[] { "Pickup", "Inspect", "--", "--" };
        item = Instantiate(baseItem);
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
        //Debug.Log("Picking up " + item.name);
        int itemsLeftAfterPickup = InventoryManager.GetInstance().GetInventory().AddItem(item);

        if (itemsLeftAfterPickup == 0)
        {
            Destroy(item);
            Destroy(transform.gameObject);
        }
        else
        {
            item.quantity = itemsLeftAfterPickup;
        }
    }
}
