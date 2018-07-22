using System.Collections.Generic;
using UnityEngine;

//this script is for the player only
public class EquipmentManager : MonoBehaviour {
    [SerializeField]
    private EquipUI UI;

    private BodyPartController myBody;
    private Inventory inv;

    [SerializeField]
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

        //add callback to either bodypart controller or each bodypart
        //GatherEquipment()
    }

    public void Equip(Item newItem)
    {
        Item.EquipmentSlot slot = newItem.myEquipSlot;
        currentEquipment[slot] = newItem;
        UI.UpdateUI(newItem, newItem.myEquipSlot);
    }

    //may need to check for naked unequip
    public void Unequip(Item.EquipmentSlot slot)
    {
        currentEquipment[slot] = null;
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

    /************/

    /*
    void Strip(Item oldItem)
    {
        Item.EquipmentSlot slot = oldItem.myEquipSlot; //relies on the Equipment.EquipmentSlot enum order staying the same
        Equipment e;// = new Equipment();
        switch (oldItem.myEquipSlot)
        {
            case Item.EquipmentSlot.Head:
                e = nakedHead;
                break;
            case Item.EquipmentSlot.Chest:
                e = nakedChest;
                break;
            case Item.EquipmentSlot.Hands:
                e = nakedHands;
                break;
            case Item.EquipmentSlot.MainHand:
                e = unarmedMain;
                break;
            case Item.EquipmentSlot.OffHand:
                e = unarmedOff;
                break;
            case Item.EquipmentSlot.Legs:
                e = nakedLegs;
                break;
            case Item.EquipmentSlot.Feet:
                e = nakedFeet;
                break;
            default:
                e = null;
                Debug.LogError("Invalid EquipSlot");
                break;
        }
        Debug.Log(e.equipSlot);
        e = Instantiate(e);
        e.Init();
        currentEquipment[slot] = e;
    }
    */

    /*
    public List<EquipmentInfo> GetEquipmentInfo()
    {
        List<EquipmentInfo> infoList = new List<EquipmentInfo>();

        foreach (KeyValuePair<Item.EquipmentSlot, Equipment> equipment in currentEquipment)
        {
            infoList.Add(equipment.Value.GetEquipmentInfo());
        }

        return infoList;
    }

    public void LoadSavedEquipment(List<EquipmentInfo> savedEquipment)
    {
        
        Equipment equipment;

        for(int i = 0; i < savedEquipment.Count; i++)
        {
            equipment = Instantiate((Equipment)Resources.Load(savedEquipment[i].filePath));
            equipment.Init();
            //equipment.quality = x;
            //equipment.condition = y;
            if (equipment != null)
                Equip(equipment);
        }
    }
    */
}
