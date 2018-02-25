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
        DialogueManager.instance.dialogueWindow.SetBool("isOpen", false); 
    }

    public void KnockPlayerOutOfDialogue()
    {
        bool dialogOpen = DialogueManager.instance.dialogueWindow.GetBool("isOpen");
        if (dialogOpen)
        {
            PlayerManager.instance.player.GetComponent<PlayerController>().incapacitated = false;
            DialogueManager.instance.dialogueWindow.SetBool("isOpen", false);
        }
    }
}
