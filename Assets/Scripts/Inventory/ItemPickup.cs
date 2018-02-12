using UnityEngine;

public class ItemPickup : Interactable {
    public Item item;

    public DefaultInteractions defaultInteraction;

    void Start()
    {
        myInteractions = new string[] { "Pickup", "Inspect", "--", "--" };
    }

    public override void Interaction(string interaction)
    {
        base.Interaction(interaction);

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
                Debug.Log("invalid " + interaction + "interaction passed to " + gameObject);
                break;
        }
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
