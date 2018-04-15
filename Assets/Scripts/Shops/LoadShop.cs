﻿using UnityEngine;

public class LoadShop : MonoBehaviour {

    public Item[] shopInventory = new Item[17];
    ShopInventory shop;
    ShopDialogue dialogueWindow;

    [HideInInspector]
    public enum MessageType { WELCOME, INV_FULL, LOW_GOLD, SUCCESS }

    [System.Serializable]
    public class MyShopDialogue
    {
        public MessageType messageType;
        public string message;
    }

    public MyShopDialogue[] shopDialogue;

    void Start()
    {
        shop = ShopInventory.instance;
        dialogueWindow = ShopDialogue.instance;
    }

    public void LoadShopInventory()
    {
        for (int i = 0; i < shopInventory.Length; i++)
        {
            if(shopInventory[i] != null)
            {
                shop.AddToFirstEmptySlot(shopInventory[i]);
            }
        }

        dialogueWindow.LoadShopDialogue(shopDialogue);
    }


}
