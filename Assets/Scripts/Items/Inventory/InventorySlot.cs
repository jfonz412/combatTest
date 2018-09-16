using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [HideInInspector]
    public int slotNum;
    public Image icon;
    protected Item item;

    private UnitStateMachine psm;

    private void Start()
    {
        psm = ScriptToolbox.GetInstance().GetPlayerManager().player.GetComponent<UnitStateMachine>();
    }

    public Item Item()
    {
        return item; //will be null if slot empty
    }

    public virtual void AddItem(Item newItem)
    {
        if (newItem == null)
        {
            Debug.Log("Item null, clearing slot in UI " + newItem);
            ClearSlot();
            return;
        }

        item = newItem;
        Debug.Log("Item being added to UI slot, it's quantity is " + item.quantity);
        Sprite itemSprite = Resources.Load<Sprite>("Images/ItemIcons/" + item.icon);
        Debug.Log("sprite for " + item + " is " + itemSprite);
        icon.sprite = itemSprite;
        icon.enabled = true;
    }

    public virtual void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
    }

    public virtual void SlotHoverOver()
    {
        InvSlotClick.instance.InventorySlotHoverOver(item);
    }

    public virtual void SlotRightClicked()
    {
        if (psm.currentState == UnitStateMachine.UnitState.InvOpen)
        {
            InvSlotClick.instance.InventorySlotRightClicked(item);
        }
        else if (psm.currentState == UnitStateMachine.UnitState.Shopping)
        {
            InvSlotClick.instance.RightClickedToSell(item);
        }
    }

    public virtual void SlotLeftClicked()
    {
        if (psm.currentState == UnitStateMachine.UnitState.InvOpen)
        {
            InvSlotClick.instance.InventorySlotLeftClicked(this);
        }
        else if (psm.currentState == UnitStateMachine.UnitState.Shopping)
        {
            InvSlotClick.instance.LeftClickedToSell(this);
        }

    }
}
