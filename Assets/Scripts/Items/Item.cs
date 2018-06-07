using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")] //allows us to create Items like we can create folders, scripts, etc.
public class Item : ScriptableObject {
    [HideInInspector]
    public Transform user;

    [SerializeField]
    protected string myFileName; //DO NOT FUCKING TOUCH!!!! RISK ERASING ALL FILENAMES IN INSPECTOR

    public Sprite icon = null;
    public int? slotNum; //allows the int to be null

    new public string name = "New Item";
    public string myDescription; 

    public float baseValue = 100f;
    public float currentValue;

    public int quantity = 1;
    public int maxQuantity = 99;
    public bool stackable;

    public enum ItemDirectory { Consumables, Economic, Weapons, Armor, Tools };
    public ItemDirectory myDirectory;

    //create seperate item classes (equipment, consumable, crafting, etc. to handle differernt use cases)
    public virtual void Use() //RMB
    {
        user = ScriptToolbox.GetInstance().GetPlayerManager().player.transform;
    }

    public virtual void OpenStatWindow(string itemLocation)
    {
        DetermineValue(itemLocation);

        InfoPanel panel = InfoPanel.instance;
        if (panel != null)
            Instantiate(Resources.Load("PopUps/ItemMenu"), panel.transform.position, Quaternion.identity, panel.transform);

        ItemMenu window = ItemMenu.instance;
        if (window != null)
            window.PopulateInfo(PackageItemInfo());
    }

    protected virtual string[] PackageItemInfo()
    {
        string[] myStats = new string[4];
        if (this != null)
        {
            if (stackable)
            {
                myStats[0] = name + " (" + quantity.ToString() + ")";
            }
            else
            {
                myStats[0] = name;
            }
            myStats[1] = "Value: " + currentValue.ToString();
            myStats[3] = myDescription;
        }
        return myStats;
    }

    private void RemoveFromInventory()
    {
        InventoryManager.GetInstance().GetInventory().RemoveAndDestroy(this);
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

    public virtual string GetResourcePath()
    {
        string directory = "Items/" + myDirectory + "/";
        return directory + myFileName;
    }

    public virtual void Init()
    {
        //put any instantiation stuff here 
    }
}
