using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [HideInInspector]
    public int slotNum;
    public Image icon;
    protected Item item;

    private Brain playerBrain;

    private void Start()
    {
        playerBrain = ScriptToolbox.GetInstance().GetPlayerManager().player.GetComponent<Brain>();
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
        if (playerBrain.ActiveState(Brain.State.Shopping))
        {
            InvSlotClick.instance.RightClickedToSell(item);
        }
        else
        {
            InvSlotClick.instance.InventorySlotRightClicked(item);
        }
    }

    public virtual void SlotLeftClicked()
    {
        if(playerBrain.ActiveState(Brain.State.Shopping))
        {
            InvSlotClick.instance.LeftClickedToSell(this);
        }
        else
        {
            InvSlotClick.instance.InventorySlotLeftClicked(this);
        }

    }
}
