using UnityEngine;

public class EquipmentManager : MonoBehaviour {

    [HideInInspector]
    public Equipment unarmedMain, unarmedOff, nakedChest, nakedLegs, nakedFeet, nakedHead; //only for player if they decide to remove armor?

    [SerializeField]
    private Equipment[] currentEquipment;

    public delegate void OnEquipmentChanged(Equipment oldItem, Equipment newItem);
    public OnEquipmentChanged onEquipmentChanged;

    private Inventory inv;

    private void Awake()
    {
        //moved to awake to ensure it loaded before Loadout tries to equip items
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];
    }

    private void Start ()
    {
        inv = InventoryManager.GetInstance().GetInventory();
    }

    //Right clicks
    public void FastEquip(Equipment newItem)
    {
        int slotNum = (int)newItem.equipSlot;
        Equipment oldItem = currentEquipment[slotNum];

        //also Strips so this comes before Equip
        Unequip(slotNum);
        Equip(newItem);
        inv.Remove(newItem);

        if(!oldItem.naked)
            inv.AddItem(oldItem);
    }

    //Right clicks
    public void FastUnequip(Equipment item)
    {
        Unequip((int)item.equipSlot);
        inv.AddItem(item);
    }

    public void Equip (Equipment newItem) {
        Equipment oldItem = null;
        int slotIndex = (int)newItem.equipSlot;
        currentEquipment[slotIndex] = newItem;

        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(oldItem, newItem);
        }
    }

    //may need to check for naked unequip
    public void Unequip(int slotIndex)
    {
        if(currentEquipment[slotIndex] != null)
        {
            Equipment oldItem = currentEquipment[slotIndex];

            Strip(oldItem);

            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(oldItem, null);    
            }
        }
    }

    void Strip(Equipment oldItem)
    {
        int slotIndex = (int)oldItem.equipSlot;

        switch (oldItem.equipSlot)
        {
            case EquipmentSlot.Head:
                currentEquipment[slotIndex] = nakedHead;
                break;
            case EquipmentSlot.Chest:
                currentEquipment[slotIndex] = nakedChest;
                break;
            case EquipmentSlot.MainHand:
                currentEquipment[slotIndex] = unarmedMain;
                break;
            case EquipmentSlot.OffHand:
                currentEquipment[slotIndex] = unarmedOff;
                break;
            case EquipmentSlot.Legs:
                currentEquipment[slotIndex] = nakedLegs;
                break;
            case EquipmentSlot.Feet:
                currentEquipment[slotIndex] = nakedFeet;
                break;
            default:
                currentEquipment[slotIndex] = null;
                Debug.LogError("Invalid EquipSlot");
                break;
        }
        //unitAnim.LoadEquipment((int)oldItem.equipSlot, 0); //adds naked/unarmed to anim slot
    }

    public Equipment EquipmentFromSlot(int slotNum)
    { 
        if(slotNum != -1)
        {
            return currentEquipment[slotNum]; 
        }
        else //failsafe, shouldn't happen though
        {
            Debug.LogWarning("Unknown equip slot passed from HumanBody");
            return nakedChest;
        }
    }

    public string[] GetEquipmentNames()
    {
        //should be a dictionary so I can store the ID and condition...or a struct that can store all kinds of data
        string[] equipmentNames = new string[6];

        for (int i = 0; i < currentEquipment.Length; i++)
        {
            equipmentNames[i] = currentEquipment[i].GetResourcePath();
        }

        return equipmentNames;
    }

    public void LoadSavedEquipment(string[] equipmentNames)
    {
        Equipment equipment;

        for (int i = 0; i < equipmentNames.Length; i++)
        {
            equipment = Instantiate((Equipment)Resources.Load(equipmentNames[i]));
            equipment.Init();
            //equipment.quality = x;
            //equipment.condition = y;
            if(equipment != null)
                Equip(equipment);
        }
    }
}
