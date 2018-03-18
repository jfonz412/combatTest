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

    //closes any inventory, equipment, dialogue, and popup windows that may be open
    public void CloseAllWindows()
    {
        DestroyPopupMenus();
        equipUI.equipmentUI.SetActive(false);
        inventoryUI.inventoryUI.SetActive(false);
        MouseSlot.instance.ToggleSprite(inventoryUI.inventoryUI.activeSelf);
        DialogueManager.instance.dialogueWindow.SetBool("isOpen", false); 
    }

    public void KnockPlayerOutOfDialogue()
    {
        PlayerManager.instance.player.GetComponent<PlayerState>().SetPlayerState(PlayerState.PlayerStates.Idle);
        DialogueManager.instance.dialogueWindow.SetBool("isOpen", false);
        ShopInventoryUI.instance.ShopUIToggle(false);
    }
}
