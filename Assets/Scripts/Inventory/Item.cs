using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")] //allows us to create Items like we can create folders, scripts, etc.
public class Item : ScriptableObject {

    new public string name = "New Item";
    public Sprite icon = null;
    public int? slotNum;
    //public int quantity;

    public bool isDefaultItem = false; //not sure what this is for, it's from the video

    [HideInInspector]
    public Transform player;

    public virtual void Use()
    {
        //player is always going to be the one using the item so might as well grab this here in the base item script
        player = PlayerManager.instance.player.transform;
        //Debug.Log("Using " + name);
    }

    public void RemoveFromInventory()
    {
        Inventory.instance.Remove(this);
    }
}
