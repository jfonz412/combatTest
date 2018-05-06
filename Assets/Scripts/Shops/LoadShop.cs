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

    void Start()
    {
        shop = InventoryManager.GetInstance().GetShopInventory();
        dialogueWindow = InventoryManager.GetInstance().GetShopDialogue();
    }

    public void LoadShopInventory()
    {
        shop.LoadShopInventory(shopInventory, this);
        dialogueWindow.LoadShopDialogue(shopDialogue);
    }

    public void UpdateInventory(List<Item> items)
    {
        Debug.Log("Updating NPC shop's inventory" + items[1]);
        shopInventory = new List<Item>(items);
    }
}
