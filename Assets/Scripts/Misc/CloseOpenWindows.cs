using UnityEngine;

public class CloseOpenWindows : MonoBehaviour {

    public Canvas canvas;
    InventoryUI inventoryUI;
    EquipUI equipUI;

    #region Singleton
    public static CloseOpenWindows instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of inventory found");
            return;
        }
        instance = this;
    }
    #endregion

    void Start()
    {
        inventoryUI = canvas.GetComponent<InventoryUI>();
        equipUI = canvas.GetComponent<EquipUI>();
    }

    public void DestroyPopupMenus()
    {
        if (InteractableMenu.instance != null)
        {
            InteractableMenu.instance.CloseMenu();
        }

        if (EquipmentStats.instance != null)
        {
            EquipmentStats.instance.CloseMenu();
        }

        if (ItemMenu.instance != null)
        {
            ItemMenu.instance.CloseMenu();
        }
    }

    //not yet used but will probably come in handy later on...
    //closes any inventory, equipment, dialogue, and popup windows that may be open
    public void CloseAllWindows()
    {
        DestroyPopupMenus();
        equipUI.equipmentUI.SetActive(false);
        inventoryUI.inventoryUI.SetActive(false);
        inventoryUI.ToggleMouseSlotSprite();
        DialogueManager.instance.isOpen = false;
    }

}
