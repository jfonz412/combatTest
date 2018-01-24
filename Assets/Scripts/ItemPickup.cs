using UnityEngine;

public class ItemPickup : Interactable {

    public override void DefaultInteraction()
    {
        base.DefaultInteraction();
        PickUp();    
    }

    void PickUp()
    {
        Debug.Log("Picking up item");
        // Add to inventory
        Destroy(gameObject);
    }
}
