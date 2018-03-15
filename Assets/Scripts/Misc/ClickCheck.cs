//Attatch this script to a Button GameObject
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickCheck : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    InventorySlot slot;

    void Start()
    {
        //need a refrence to parent's InventorySlot.cs
        slot = GetComponentInParent<InventorySlot>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        CloseOpenWindows.instance.DestroyPopupMenus();
        //Debug.Log("entered " + name);
        slot.SlotHoverOver();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("exiting " + name);
        CloseOpenWindows.instance.DestroyPopupMenus();
    }

    //Detect if a click occurs
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        CloseOpenWindows.instance.DestroyPopupMenus();

        //Use this to tell when the user right-clicks on the Button
        if (pointerEventData.button == PointerEventData.InputButton.Right)
        {
            slot.SlotRightClicked(); 
        }

        //Use this to tell when the user left-clicks on the Button
        if (pointerEventData.button == PointerEventData.InputButton.Left)
        {
            slot.SlotLeftClicked();
        }
    }
}
