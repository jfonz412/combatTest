//Attatch this script to a Button GameObject
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickCheck : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    InventorySlot slot;

    PlayerState.PlayerStates[] invalidStates = new PlayerState.PlayerStates[]
{
        PlayerState.PlayerStates.Speaking,
        PlayerState.PlayerStates.Dead,
        PlayerState.PlayerStates.Prompt
};

    void Start()
    {
        slot = GetComponentInParent<InventorySlot>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        CloseOpenWindows.instance.DestroyPopupMenus();
        slot.SlotHoverOver();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CloseOpenWindows.instance.DestroyPopupMenus();
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (InvalidState())
        {
            return;
        }

        CloseOpenWindows.instance.DestroyPopupMenus();

        if (pointerEventData.button == PointerEventData.InputButton.Right)
        {
            slot.SlotRightClicked(); 
        }

        if (pointerEventData.button == PointerEventData.InputButton.Left)
        {
            slot.SlotLeftClicked();
        }
    }

    bool InvalidState()
    {
        return PlayerState.CheckPlayerState(invalidStates);
    }
}
