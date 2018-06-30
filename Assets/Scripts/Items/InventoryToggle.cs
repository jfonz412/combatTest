using UnityEngine;

public class InventoryToggle : MonoBehaviour {

    public GameObject inventoryUI;
    public GameObject equipUI;
    public GameObject infoPanelUI;
    public GameObject healthPanel;

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
            ToggleWindows(inventoryUI.activeSelf);
            
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
        ToggleWindows(true);
    }

    public void CloseInventory()
    {
        inventoryUI.SetActive(false);
        ToggleWindows(false);
    }

    private void ToggleWindows(bool b)
    {
        equipUI.SetActive(b);
        infoPanelUI.SetActive(b);
        healthPanel.SetActive(b);
        MouseSlot.instance.ToggleSprite(b);
    }
}
