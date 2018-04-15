using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryToggle : MonoBehaviour {

    public GameObject inventoryUI;
    public GameObject equipUI;
    public GameObject infoPanelUI;

    PlayerState.PlayerStates[] invalidStates = new PlayerState.PlayerStates[]
    {
        PlayerState.PlayerStates.Dead,
        PlayerState.PlayerStates.Speaking
    };

    #region Singleton
    public static InventoryToggle instance;

    void Awake()
    {
        instance = this;
    }
    #endregion

    // Update is called once per frame
    void Update()
    {
        InventoryUIToggle();
    }

    void InventoryUIToggle()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
            equipUI.SetActive(inventoryUI.activeSelf);
            infoPanelUI.SetActive(inventoryUI.activeSelf);
            MouseSlot.instance.ToggleSprite(inventoryUI.activeSelf);

            CloseOpenWindows.instance.DestroyPopupMenus();
        }

        CheckForValidPlayerState();
    }

    public void CloseInventory()
    {
        inventoryUI.SetActive(false);
        equipUI.SetActive(false);
        infoPanelUI.SetActive(false);
        MouseSlot.instance.ToggleSprite(false);
    }

    void CheckForValidPlayerState()
    {
        if (PlayerState.CheckPlayerState(invalidStates))
        {
            inventoryUI.SetActive(false);
            MouseSlot.instance.ToggleSprite(inventoryUI.activeSelf);
        }
    }

}
