using UnityEngine;

public class InventoryToggle : MonoBehaviour {

    public GameObject inventoryUI;
    public GameObject equipUI;
    public GameObject infoPanelUI;
    public GameObject healthPanel;
    public SkillPanel skillPanel;
    private UnitStateMachine psm;

    private void Start()
    {
        psm = ScriptToolbox.GetInstance().GetPlayerManager().player.GetComponent<UnitStateMachine>();
    }

    private void Update()
    {
        InventoryUIToggle();
    }

    private void InventoryUIToggle()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            if (inventoryUI.activeSelf) //if already active go to idle
            {
                psm.RequestChangeState(UnitStateMachine.UnitState.Idle);
            }
            else
            {
                psm.RequestChangeState(UnitStateMachine.UnitState.InvOpen);
            }
            ScriptToolbox.GetInstance().GetWindowCloser().DestroyPopupMenus();
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
        skillPanel.Toggle(b);
        MouseSlot.instance.ToggleSprite(b);
    }
}
