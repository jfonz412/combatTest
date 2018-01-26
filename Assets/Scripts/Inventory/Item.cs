// this script can potentially hold all of an items stats, but I would need to 
// link this Item up with the Weapon and Armor scripts/prefabs already established...
// maybe this script can be where the equipment stats live?
// must also account for non-combat/economic/crafting type items

using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")] //allows us to create Items like we can create folders, scripts, etc.
public class Item : ScriptableObject {
    new public string name = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem = false; //not sure what this is for, it's from the video
    //public int equipmentID

    public virtual void Use()
    {
        //Use item
        //Something might happen

        Debug.Log("Using " + name);
    }
}
