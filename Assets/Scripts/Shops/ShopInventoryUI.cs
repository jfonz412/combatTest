using UnityEngine;
using UnityEngine.UI;

public class ShopInventoryUI : MonoBehaviour {

    public Transform itemsParent;
    public GameObject shopUI;
    public GameObject shopDialogue;
    public Text panel;

    private ShopInventory shop;
    private ShopSlot[] slots;

    private Brain playerBrain;

    #region Singleton

    public static ShopInventoryUI instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of SHOP inventoryUI found");
            return;
        }
        instance = this;
    }
    #endregion

    private void Start()
    {
        shop = InventoryManager.GetInstance().GetShopInventory();
        shop.onInventoryChanged += UpdateUI;
        slots = itemsParent.GetComponentsInChildren<ShopSlot>();
        playerBrain = ScriptToolbox.GetInstance().GetPlayerManager().player.GetComponent<Brain>();

        AssignSlotNums();
    }

    public void ShopUIToggle(bool active, string NPCname = null)
    {
        if (NPCname != null)
            panel.text = NPCname;

        shopUI.SetActive(active);
        shopDialogue.SetActive(active);

        if (!active)
        {
            playerBrain.ToggleState(Brain.State.Neutral, false);
            playerBrain.ToggleState(Brain.State.Shopping, false);
            shop.ClearShopInventory();
        }
        else
        {
            playerBrain.ToggleState(Brain.State.Neutral, true);
            playerBrain.ToggleState(Brain.State.Shopping, true);
            ScriptToolbox.GetInstance().GetWindowCloser().DestroyPopupMenus(); 
        }
    }

    //for button
    public void ExitShop()
    {
        ShopUIToggle(false);
    }

    //this method loads the UI slots with items from ShopInventory, called from ShopInventory delgate
    private void UpdateUI()
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

    private void AssignSlotNums()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].slotNum = i;
        }
    }
}
