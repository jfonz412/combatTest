using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable", menuName = "Inventory/Consumable Item")]
public class Consumable : Item {

    [SerializeField]
    private ItemEffects.ItemEffect myEffect;

    public override void Use()
    {
        base.Use();
        ItemEffects.TriggerEffect(myEffect);
        InventoryManager.GetInstance().GetInventory().CondenseStackables(this, 1);
        Debug.Log("Using " + name);
    }

    public override void OpenStatWindow(string itemLocation)
    {
        base.OpenStatWindow(itemLocation);

        ItemMenu window = ItemMenu.instance;
        if (window != null)
            window.PopulateInfo(PackageItemInfo());
    }

    string[] PackageItemInfo()
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
}
