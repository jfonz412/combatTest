﻿using UnityEngine;
using UnityEngine.UI;

public class ShopInventoryUI : MonoBehaviour {

    public Transform itemsParent;
    public GameObject shopUI;
    public GameObject shopDialogue;
    public Text panel;

    ShopInventory shop;
    ShopSlot[] slots;

    #region Singleton

    public static ShopInventoryUI instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of SHOP inventoryUI found");
            return;
        }
        instance = this;
    }
    #endregion

    // Use this for initialization
    void Start()
    {
        shop = InventoryManager.GetInstance().GetShopInventory();
        shop.onInventoryChanged += UpdateUI;
        slots = itemsParent.GetComponentsInChildren<ShopSlot>();
        AssignSlotNums();
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < shop.items.Count)
            {
                slots[i].AddItem(shop.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }

    public void OpenShop(string NPCname)
    {
        panel.text = NPCname;
        ShopUIToggle(true);
    }

    public void ShopUIToggle(bool active)
    {
        //to be trigged by NPC interaction
        shopUI.SetActive(active);
        shopDialogue.SetActive(active);

        if (!active)
        {
            PlayerState.SetPlayerState(PlayerState.PlayerStates.Idle);
            shop.ClearShopInventory();
        }
        else
        {
            ScriptToolbox.GetInstance().GetWindowCloser().DestroyPopupMenus(); 
        }
    }

    void AssignSlotNums()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].slotNum = i;
        }
    }
}
