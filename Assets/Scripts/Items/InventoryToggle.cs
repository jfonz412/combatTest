using UnityEngine;

public class InventoryToggle : MonoBehaviour {

    public GameObject inventoryUI;
    public GameObject equipUI;
    public GameObject infoPanelUI;

    private PlayerState.PlayerStates[] invalidStates = new PlayerState.PlayerStates[]
    {
        PlayerState.PlayerStates.Dead,
        PlayerState.PlayerStates.Speaking,
        //PlayerState.PlayerStates.Paused
    };

    private void Update()
    {
        InventoryUIToggle();
    }

    private void InventoryUIToggle()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
            equipUI.SetActive(inventoryUI.activeSelf);
            infoPanelUI.SetActive(inventoryUI.activeSelf);
            MouseSlot.instance.ToggleSprite(inventoryUI.activeSelf);

            ScriptToolbox.GetInstance().GetWindowCloser().DestroyPopupMenus();
        }

        CheckForValidPlayerState();
    }

    private void CheckForValidPlayerState()
    {
        if (PlayerState.CheckPlayerState(invalidStates))
        {
            CloseInventory();
        }

        if (PlayerState.GetPlayerState() == PlayerState.PlayerStates.Shopping)
        {
            OpenInventory();
        }
    }

    public void OpenInventory()
    {
        inventoryUI.SetActive(true);
        equipUI.SetActive(true);
        infoPanelUI.SetActive(true);
        MouseSlot.instance.ToggleSprite(true);
    }

    public void CloseInventory()
    {
        inventoryUI.SetActive(false);
        equipUI.SetActive(false);
        infoPanelUI.SetActive(false);
        MouseSlot.instance.ToggleSprite(false);
    }
}
