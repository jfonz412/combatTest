using System.Collections.Generic;
using UnityEngine;

public class LoadShop : MonoBehaviour {
    [SerializeField]
    private List<Item> shopInventory = new List<Item>();
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
        InventoryManager.GetInstance().GetShopInventory().LoadShopInventory(shopInventory, this);
        InventoryManager.GetInstance().GetShopDialogue().LoadShopDialogue(shopDialogue);
    }

    public void UpdateInventory(List<Item> items)
    {
        //Debug.Log("Updating NPC shop's inventory" + items[1]);
        shopInventory = new List<Item>(items);
    }

    public SavedItem[] GetCurrentInventory()
    {
        SavedItem[] itemInfo = new SavedItem[shopInventory.Count];

        for (int i = 0; i < shopInventory.Count; i++)
        {
            if (shopInventory[i] != null)
            {
                //itemInfo[i].fileName = shopInventory[i].GetResourcePath();
                itemInfo[i].quantity = shopInventory[i].quantity;
                Debug.Log("saved shop inventory " + itemInfo[i].fileName);
            }
        }
        return itemInfo;
    }
}
