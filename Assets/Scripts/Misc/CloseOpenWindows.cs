using UnityEngine;

public class CloseOpenWindows : MonoBehaviour {

    public Canvas canvas;
    private EquipUI equipUI;
    private ScriptToolbox toolbox;

    void Start()
    {
        toolbox = ScriptToolbox.GetInstance();
    }

    public void DestroyPopupMenus()
    {
        //these are probably fine as singletons, the alternative would be to have these be in the toolbox, and they talk to 
        //a script that is on the actual gameobject and can access the children. It will decouple things slightly

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

        SelectInteractable.Destroy();
    }

    //closes any inventory, equipment, dialogue, and popup windows that may be open
    public void CloseAllWindows()
    {
        DestroyPopupMenus();
        InventoryManager.GetInstance().GetInventoryToggle().CloseInventory();
        toolbox.GetDialogueManager().dialogueWindow.SetBool("isOpen", false);
    }

    public void KnockPlayerOutOfDialogue()
    {
        PlayerState.SetPlayerState(PlayerState.PlayerStates.Idle);
        toolbox.GetDialogueManager().dialogueWindow.SetBool("isOpen", false);
        ShopInventoryUI.instance.ShopUIToggle(false);
    }
}
