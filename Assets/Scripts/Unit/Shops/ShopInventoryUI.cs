using UnityEngine;
using UnityEngine.UI;

public class ShopInventoryUI : MonoBehaviour {

    public Transform itemsParent;
    public GameObject shopUI;
    public Text panel;

    ShopInventory shop;
    ShopSlot[] slots;

    PlayerState playerState;

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
        playerState = PlayerManager.instance.player.GetComponent<PlayerState>();
        shop = ShopInventory.instance;
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

        //shouldn't allow user to pick items off shop without suffecient funds (or space)?
        //MouseSlot.instance.ToggleSprite(shopUI.activeSelf); 

        CloseOpenWindows.instance.DestroyPopupMenus();

        if (!active)
        {
            playerState.SetPlayerState(PlayerState.PlayerStates.Idle);
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
