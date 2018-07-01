using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour {

    [SerializeField]
    private Equipment unarmedMain, unarmedOff, nakedChest, nakedLegs, nakedFeet, nakedHead, nakedHands; //only for player if they decide to remove armor

    [SerializeField]
    private Dictionary<EquipmentSlot, Equipment> currentEquipment = new Dictionary<EquipmentSlot, Equipment>
    {
        { EquipmentSlot.Head, null },
        { EquipmentSlot.Chest, null },
        { EquipmentSlot.Hands, null },
        { EquipmentSlot.MainHand, null },
        { EquipmentSlot.OffHand, null },
        { EquipmentSlot.Legs, null },
        { EquipmentSlot.Feet, null }
    };

    public delegate void OnEquipmentChanged(Equipment oldItem, Equipment newItem);
    public OnEquipmentChanged onEquipmentChanged;

    private Inventory inv;

    private void Start ()
    {
        inv = InventoryManager.GetInstance().GetInventory();
    }

    //Right clicks
    public void FastEquip(Equipment newItem)
    {
        EquipmentSlot slot = newItem.equipSlot;
        Equipment oldItem = currentEquipment[slot];

        //also Strips so this comes before Equip
        Unequip(slot);
        Equip(newItem);
        inv.Remove(newItem);

        if(!oldItem.naked)
            inv.AddItem(oldItem);
    }

    //Right clicks
    public void FastUnequip(Equipment item)
    {
        Unequip(item.equipSlot);
        inv.AddItem(item);
    }

    public void Equip (Equipment newItem) {
        Equipment oldItem = null;
        EquipmentSlot slot = newItem.equipSlot;
        currentEquipment[slot] = newItem;

        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(oldItem, newItem);
        }
    }

    //may need to check for naked unequip
    public void Unequip(EquipmentSlot slot)
    {
        if(currentEquipment[slot] != null)
        {
            Equipment oldItem = currentEquipment[slot];

            Strip(oldItem);

            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(oldItem, null);    
            }
        }
    }

    void Strip(Equipment oldItem)
    {
        EquipmentSlot slot = oldItem.equipSlot; //relies on the Equipment.EquipmentSlot enum order staying the same
        Equipment e;// = new Equipment();
        switch (oldItem.equipSlot)
        {
            case EquipmentSlot.Head:
                e = nakedHead;
                break;
            case EquipmentSlot.Chest:
                e = nakedChest;
                break;
            case EquipmentSlot.Hands:
                e = nakedHands;
                break;
            case EquipmentSlot.MainHand:
                e = unarmedMain;
                break;
            case EquipmentSlot.OffHand:
                e = unarmedOff;
                break;
            case EquipmentSlot.Legs:
                e = nakedLegs;
                break;
            case EquipmentSlot.Feet:
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

    public Equipment EquipmentFromSlot(EquipmentSlot slot)
    { 
         return currentEquipment[slot]; 
    }

    public List<EquipmentInfo> GetEquipmentInfo()
    {
        List<EquipmentInfo> infoList = new List<EquipmentInfo>();

        foreach (KeyValuePair<EquipmentSlot, Equipment> equipment in currentEquipment)
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
}
