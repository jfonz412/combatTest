using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour {

    [HideInInspector]
    public Equipment unarmedMain, unarmedOff, nakedChest, nakedLegs, nakedFeet, nakedHead, nakedHands; //only for player if they decide to remove armor?

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

    private void Awake()
    {
        //moved to awake to ensure it loaded before Loadout tries to equip items
        //int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        //currentEquipment = new Equipment[numSlots];
    }

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
 
        switch (oldItem.equipSlot)
        {
            case EquipmentSlot.Head:
                currentEquipment[slot] = nakedHead;
                break;
            case EquipmentSlot.Chest:
                currentEquipment[slot] = nakedChest;
                break;
            case EquipmentSlot.Hands:
                currentEquipment[slot] = nakedHands;
                break;
            case EquipmentSlot.MainHand:
                currentEquipment[slot] = unarmedMain;
                break;
            case EquipmentSlot.OffHand:
                currentEquipment[slot] = unarmedOff;
                break;
            case EquipmentSlot.Legs:
                currentEquipment[slot] = nakedLegs;
                break;
            case EquipmentSlot.Feet:
                currentEquipment[slot] = nakedFeet;
                break;
            default:
                currentEquipment[slot] = null;
                Debug.LogError("Invalid EquipSlot");
                break;
        }
        //unitAnim.LoadEquipment((int)oldItem.equipSlot, 0); //adds naked/unarmed to anim slot
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
            Debug.Log(equipment.Value);
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
