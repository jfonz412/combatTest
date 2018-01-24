// this script can potentially hold all of an items stats, but I would need to 
// link this Item up with the Weapon and Armor scripts/prefabs already established...
// maybe this script can be where the equipment stats live?
// must also account for non-combat/economic/crafting type items

using UnityEngine;

//allows us to create Items like we can create folders, scripts, etc.
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject {

    new public string name = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem = false; //might not need this
    //public int equipmentID
}
