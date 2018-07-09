//Attatch this script to a Button GameObject
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickCheck : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private InventorySlot slot;
    private CloseOpenWindows windowCloser;
    private Brain playerBrain;

    Brain.State[] invalidStates = new Brain.State[]
    {
        Brain.State.Talking,
        Brain.State.Dead,
        Brain.State.Prompted
        //paused?
    };

    private void Start()
    {

        slot = GetComponentInParent<InventorySlot>();
        windowCloser = ScriptToolbox.GetInstance().GetWindowCloser();
        playerBrain = ScriptToolbox.GetInstance().GetPlayerManager().player.GetComponent<Brain>();
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
        return playerBrain.ActiveStates(invalidStates);
    }
}
