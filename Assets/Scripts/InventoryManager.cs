using UnityEngine;

public class InventoryManager : MonoBehaviour {

    private static InventoryManager instance;
    private Inventory inventory;
    private ShopInventory shop;
    private ShopDialogue shopDialogue;
    private QuantityPrompt qntyPrompt;
    private InvSlotClick invSlotClick;
    private EquipSlotClick equipSlotClick;
    private ShopSlotClick shopSlotClick;
    private SlotClickHelpers slotClickHelpers;
    private InventoryToggle invToggle;
    private CheckInventorySpace checkInvSpace;

    #region Singleton

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of InventoryManager found");
            return;
        }
        instance = this;
    }
    #endregion

    void Start()
    {
        inventory = GetComponent<Inventory>();
        shop = GetComponent<ShopInventory>();
        qntyPrompt = GetComponent<QuantityPrompt>();
        invSlotClick = GetComponent<InvSlotClick>();
        equipSlotClick = GetComponent<EquipSlotClick>();
        shopSlotClick = GetComponent<ShopSlotClick>();
        slotClickHelpers = GetComponent<SlotClickHelpers>();
        invToggle = GetComponent<InventoryToggle>();
        shopDialogue = GetComponent<ShopDialogue>();
        checkInvSpace = GetComponent<CheckInventorySpace>();
    }

    public static InventoryManager GetInstance()
    {
        return instance;
    }

    public Inventory GetInventory()
    {
        return inventory;
    }

    public ShopInventory GetShopInventory()
    {
        return shop;
    }
    public ShopDialogue GetShopDialogue()
    {
        return shopDialogue;
    }

    public QuantityPrompt GetQuantityPrompt()
    {
        return qntyPrompt;
    }

    public InvSlotClick GetInvSlotClick()
    {
        return invSlotClick;
    }

    public EquipSlotClick GetEquipSlotClick()
    {
        return equipSlotClick;
    }

    public ShopSlotClick GetShopSlotClick()
    {
        return shopSlotClick;
    }

    public SlotClickHelpers GetSlotClickHelpers()
    {
        return slotClickHelpers;
    }

    public InventoryToggle GetInventoryToggle()
    {
        return invToggle;
    }

    public CheckInventorySpace GetInventorySpaceChecker()
    {
        return checkInvSpace;
    }
}
