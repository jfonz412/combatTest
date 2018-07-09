using UnityEngine;

public class InventoryToggle : MonoBehaviour {

    public GameObject inventoryUI;
    public GameObject equipUI;
    public GameObject infoPanelUI;
    public GameObject healthPanel;
    public SkillPanel skillPanel;

    private Brain playerBrain;

    private Brain.State[] invalidStates = new Brain.State[]
    {
        Brain.State.Dead,
        Brain.State.Talking,
        //Brain.State.Paused
    };

    private void Start()
    {
        playerBrain = ScriptToolbox.GetInstance().GetPlayerManager().playerBrain;
    }

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
        if (playerBrain.ActiveStates(invalidStates))
        {
            CloseInventory();
        }
        /*
        if (playerBrain.ActiveState(Brain.State.Shopping))
        {
            OpenInventory();
        }
        */
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
        skillPanel.Toggle(b);
        MouseSlot.instance.ToggleSprite(b);
    }
}
