using System.Collections.Generic;
using UnityEngine;

//this script is for the player only
public class EquipmentManager : MonoBehaviour {
    private EquipUI UI;

    private BodyPartController myBody;
    private Inventory inv;

    public delegate void OnEquipmentChanged();
    public OnEquipmentChanged onEquipmentChanged;

    private Dictionary<Item.EquipmentSlot, Item> currentEquipment = new Dictionary<Item.EquipmentSlot, Item>
    {
        { Item.EquipmentSlot.Head, null },
        { Item.EquipmentSlot.Chest, null },
        { Item.EquipmentSlot.Hands, null },
        { Item.EquipmentSlot.MainHand, null },
        { Item.EquipmentSlot.OffHand, null },
        { Item.EquipmentSlot.Legs, null },
        { Item.EquipmentSlot.Feet, null }
    };

    private void Start()
    {
        inv = InventoryManager.GetInstance().GetInventory();
        myBody = GetComponent<BodyPartController>();
        UI = CanvasUI.instance.GetComponent<EquipUI>();
    }

    public void Equip(Item newItem)
    {
        Item.EquipmentSlot slot = newItem.myEquipSlot;
        if (slot == Item.EquipmentSlot.Head)
            Debug.LogError("Should not be equipping to head " + newItem.name);

        currentEquipment[slot] = newItem;
        if(UI == null)
            UI = CanvasUI.instance.GetComponent<EquipUI>();
        UI.UpdateUI(newItem, newItem.myEquipSlot);
        SendToBodyPart(newItem);
    }

    //may need to check for naked unequip
    public void Unequip(Item.EquipmentSlot slot)
    {
        currentEquipment[slot] = null;
        myBody.StripEquipment(slot);
        UI.UpdateUI(null, slot);
    }

    //Right clicks
    public void FastEquip(Item newItem)
    {
        //get the slot this wants to go in
        Item.EquipmentSlot slot = newItem.myEquipSlot;

        inv.AddItem(currentEquipment[slot]); //add old item to inventory
        Equip(newItem); //save over old item
        inv.Remove(newItem); //remove from inventory
    }

    //Right clicks
    public void FastUnequip(Item item)
    {
        Unequip(item.myEquipSlot);
        inv.AddItem(item);
    }

    public Item EquipmentFromSlot(Item.EquipmentSlot slot)
    {
        return currentEquipment[slot]; //may be null, but I may not need this method?
    }

    private void SendToBodyPart(Item item)
    {
        if(myBody == null)
            myBody = GetComponent<BodyPartController>();

        if (item.myEquipSlot == Item.EquipmentSlot.MainHand || item.myEquipSlot == Item.EquipmentSlot.OffHand)
        {
            myBody.EquipWeapon(item);
        }
        else
        {
            myBody.EquipArmor(item);
        }
    }

    public void LoadSavedEquipment(BodyPart.PartInfo[] parts)
    {

        //not loading default equipment if nothing is saved
        for(int i = 0; i < parts.Length; i++)
        {
            if(parts[i].myArmor != null)
                Equip(parts[i].myArmor);
            if (parts[i].myWeapon != null)
                Equip(parts[i].myWeapon);
        }
    }
}
