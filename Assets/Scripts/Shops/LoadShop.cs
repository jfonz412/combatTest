using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class LoadShop : MonoBehaviour {
    [SerializeField]
    private List<Item> shopInventory = new List<Item>(); //need to get this from static script using this unit's name to get the proper list
    private ShopInventory shop;
    private ShopDialogue dialogueWindow;

    [HideInInspector]
    public enum MessageType { WELCOME, INV_FULL, LOW_GOLD, SUCCESS, INVAL_QNTY }

    [System.Serializable]
    public class MyShopDialogue
    {
        public MessageType messageType;
        public string message;
    }

    public MyShopDialogue[] shopDialogue;

    public void LoadShopInventory()
    {
        shopInventory = DemoburghShops.GetInventory(gameObject.name).ToList();
        InventoryManager.GetInstance().GetShopInventory().LoadShopInventory(shopInventory, this); //load inventory window with this
        InventoryManager.GetInstance().GetShopDialogue().LoadShopDialogue(shopDialogue); //get shop dialog window
    }

    public void UpdateInventory(List<Item> items) //WHERE IS THIS CALLED? FROM SHOPINVENTORY.CS I think
    {
        //Debug.Log("Updating NPC shop's inventory" + items[1]);
        shopInventory = new List<Item>(items);
    }

    public Item[] GetCurrentInventory()
    {
        Item[] itemInfo = new Item[shopInventory.Count];

        for (int i = 0; i < shopInventory.Count; i++)
        {
            if (shopInventory[i] != null)
            {
                //itemInfo[i].fileName = shopInventory[i].GetResourcePath();
                itemInfo[i].quantity = shopInventory[i].quantity;
            }
        }
        Debug.Log("needs to be redone");
        return itemInfo;
    }
}
