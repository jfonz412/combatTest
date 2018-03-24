using UnityEngine;

public class LoadShop : MonoBehaviour {

    ShopInventory shop;
    public Item[] shopInventory = new Item[17];

    void Start()
    {
        shop = ShopInventory.instance;
    }

    public void LoadShopInventory()
    {
        shop.ClearShopInventory();
        for (int i = 0; i < shopInventory.Length; i++)
        {
            if(shopInventory[i] != null)
            {
                shop.AddToFirstEmptySlot(shopInventory[i]);
            }
        }
    }
}
