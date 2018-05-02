using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")] //allows us to create Items like we can create folders, scripts, etc.
public class Item : ScriptableObject {
    [HideInInspector]
    public Transform user;

    public Sprite icon = null;
    public int? slotNum; //allows the int to be null

    new public string name = "New Item";
    public string myDescription; 

    public float baseValue = 100f;
    public float currentValue;

    public int quantity = 1;
    public int maxQuantity = 99;
    public bool stackable;

    //create seperate item classes (equipment, consumable, crafting, etc. to handle differernt use cases)
    public virtual void Use() //RMB
    {
        user = ScriptToolbox.GetInstance().GetPlayerManager().player.transform;
    }

    public virtual void OpenStatWindow(string itemLocation)
    {
        DetermineValue(itemLocation);

        InfoPanel panel = InfoPanel.instance;
        if(panel != null)
        {
            Instantiate(Resources.Load("ItemMenu"), panel.transform.position, Quaternion.identity, panel.transform);
        }
    }

    void RemoveFromInventory()
    {
        Inventory.instance.RemoveAndDestroy(this);
    }

    public void DetermineValue(string itemLocation)
    {
        if (itemLocation == "Shop")
        {
            currentValue = PriceChecker.AppraiseItem(this, "Purchase");
        }
        else
        {
            currentValue = PriceChecker.AppraiseItem(this, "Sale");
        }
    }
}
