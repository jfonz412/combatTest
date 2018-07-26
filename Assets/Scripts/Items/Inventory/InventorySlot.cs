﻿using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [HideInInspector]
    public int slotNum;
    public Image icon;
    public Item item;

    private Brain playerBrain;

    private void Start()
    {
        playerBrain = ScriptToolbox.GetInstance().GetPlayerManager().player.GetComponent<Brain>();
    }

    public virtual void AddItem(Item newItem)
    {
        if (newItem == null)
        {
            ClearSlot();
            return;
        }

        item = newItem;
        //icon.sprite = item.icon;
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
