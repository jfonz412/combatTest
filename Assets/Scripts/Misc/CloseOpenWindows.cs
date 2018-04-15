using UnityEngine;

public class CloseOpenWindows : MonoBehaviour {

    public Canvas canvas;
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
        InventoryToggle.instance.CloseInventory();
        DialogueManager.instance.dialogueWindow.SetBool("isOpen", false); 
    }

    public void KnockPlayerOutOfDialogue()
    {
        PlayerState.SetPlayerState(PlayerState.PlayerStates.Idle);
        DialogueManager.instance.dialogueWindow.SetBool("isOpen", false);
        ShopInventoryUI.instance.ShopUIToggle(false);
    }
}
