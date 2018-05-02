//Attatch this script to a Button GameObject
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickCheck : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private InventorySlot slot;
    private CloseOpenWindows windowCloser;

    PlayerState.PlayerStates[] invalidStates = new PlayerState.PlayerStates[]
{
        PlayerState.PlayerStates.Speaking,
        PlayerState.PlayerStates.Dead,
        PlayerState.PlayerStates.Prompt
};

    void Start()
    {
        slot = GetComponentInParent<InventorySlot>();
        windowCloser = ScriptToolbox.GetInstance().GetWindowCloser();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        windowCloser.DestroyPopupMenus();
        slot.SlotHoverOver();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        windowCloser.DestroyPopupMenus();
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (InvalidState())
        {
            return;
        }

        windowCloser.DestroyPopupMenus();

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
