using UnityEngine;
using UnityEngine.UI;

public class ShopInventoryUI : MonoBehaviour {

    public Transform itemsParent;
    public GameObject shopUI;
    public GameObject shopDialogue;
    public Text panel;

    private ShopInventory shop;
    private ShopSlot[] slots;

    private PlayerStateMachine psm;

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
        psm = ScriptToolbox.GetInstance().GetPlayerManager().playerStateMachine;

        AssignSlotNums();
    }

    //wrapper for button
    public void ExitShop()
    {
        ShopUIToggle(false);
    }

    public void ShopUIToggle(bool active, string NPCname = null)
    {
        if (NPCname != null)
            panel.text = NPCname;

        shopUI.SetActive(active);
        shopDialogue.SetActive(active);

        if (!active)
        {
            shop.ClearShopInventory();  //shopOwner's state is changed in here
            InventoryManager.GetInstance().GetInventoryToggle().CloseInventory();
            psm.RequestChangeState(UnitStateMachine.UnitState.Idle);
        }
        else
        {
            ScriptToolbox.GetInstance().GetWindowCloser().DestroyPopupMenus(); 
        }
    }

    //for units to call in their own exit states if knocked out of shopping
    public void UnitExitingShopState(bool unitIsPlayer = false)
    {
        //this method will always be called even after pressing the exit button because each unit's OnStateExit calls this
        //the if statement prevents it from going any further if the shop is already inactive
        if (shopUI.activeSelf)
        {
            shopUI.SetActive(false);
            shopDialogue.SetActive(false);
            InventoryManager.GetInstance().GetInventoryToggle().CloseInventory();
            shop.ClearShopInventory();
        }

        if (!unitIsPlayer)
        {
            PlayerStateMachine psm = ScriptToolbox.GetInstance().GetPlayerManager().playerStateMachine;
            psm.RequestChangeState(UnitStateMachine.UnitState.Idle);
        }
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
