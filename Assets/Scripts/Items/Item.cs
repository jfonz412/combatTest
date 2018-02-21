using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")] //allows us to create Items like we can create folders, scripts, etc.
public class Item : ScriptableObject {

    [HideInInspector]
    public Transform player;

    new public string name = "New Item";
    public Sprite icon = null;
    public int? slotNum;
    //public int quantity;

    public virtual void Use()
    {
        //player is always going to be the one using the item so might as well grab this here in the base item script
        player = PlayerManager.instance.player.transform;

        //Item.Use() is for Right Clicks and will do different things depending on the item (consumable, equipment, etc.) 
        //will need more classes that inherit from Item to get more item variety

        //Debug.Log("Using " + name);
    }

    public void RemoveFromInventory()
    {
        Inventory.instance.Remove(this);
    }
}
