using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")] //allows us to create Items like we can create folders, scripts, etc.
public class Item : ScriptableObject {
    new public string name = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem = false; //not sure what this is for, it's from the video

    //for now just the player will be using/equipping items from the singleton inventory
    [HideInInspector]
    public Transform player;

    public virtual void Use()
    {
        //Use item
        //Something might happen
        player = GameObject.Find("Player").transform;
        Debug.Log("Using " + name);
    }

    public void RemoveFromInventory()
    {
        Inventory.instance.Remove(this);
    }
}
