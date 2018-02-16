//Attatch this script to a Button GameObject
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickCheck : MonoBehaviour, IPointerClickHandler
{
    InventorySlot slot; //must account for equipslot also....maybe make equip and slot inherit from Slot.cs script?

    void Start()
    {
        //need a refrence to parent's InventorySlot.cs
        slot = GetComponentInParent<InventorySlot>();
    }
    //Detect if a click occurs
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        //Use this to tell when the user right-clicks on the Button
        if (pointerEventData.button == PointerEventData.InputButton.Right)
        {
            //Output to console the clicked GameObject's name and the following message. You can replace this with your own actions for when clicking the GameObject.
            Debug.Log(name + " Game Object Right Clicked!");
            slot.UseItem();
        }

        //Use this to tell when the user left-clicks on the Button
        if (pointerEventData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log(name + " Game Object Left Clicked!");
            slot.SwapWithMouseSlot();
        }
    }
}
