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
        //make this a static class?
        if (InteractableMenu.instance != null)
        {
            InteractableMenu.instance.CloseMenu();
        }

        //make this a static class?
        if (EquipmentStats.instance != null)
        {
            EquipmentStats.instance.CloseMenu();
        }

        //make this a static class?
        if (ItemMenu.instance != null)
        {
            ItemMenu.instance.CloseMenu();
        }

        SelectInteractable.Destroy();
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
