using UnityEngine;

public class EquipmentManager : MonoBehaviour {

    [HideInInspector]
    public Equipment unarmedMain, unarmedOff, nakedChest, nakedLegs, nakedFeet, nakedHead; //only for player
 
    UnitAnimator unitAnim;

    public Equipment[] currentEquipment;

    public delegate void OnEquipmentChanged(Equipment oldItem, Equipment newItem);
    public OnEquipmentChanged onEquipmentChanged;


    void Start () {
        unitAnim = GetComponent<UnitAnimator>();

        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];
    }

    //Right clicks
    public void FastEquip(Equipment newItem)
    {
        Inventory inv = Inventory.instance;

        int slotNum = (int)newItem.equipSlot;
        Equipment oldItem = currentEquipment[slotNum];

        //also Strips so this comes before Equip
        Unequip(slotNum); 

        Equip(newItem);
        inv.Remove(newItem);

        if(oldItem.equipmentID != 0) //0 is unarmed/naked
            inv.PickupItem(oldItem);
    }

    //Right clicks
    public void FastUnequip(Equipment item)
    {
        Inventory inv = Inventory.instance;

        if (item.equipmentID != 0) //0 is unarmed/naked
        {
            Unequip((int)item.equipSlot);
            inv.PickupItem(item);
        }
    }

    public void Equip (Equipment newItem) {
        Equipment oldItem = null;
        int slotIndex = (int)newItem.equipSlot;

        currentEquipment[slotIndex] = newItem;
        unitAnim.LoadEquipment((int)newItem.equipSlot, newItem.equipmentID);

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
        unitAnim.LoadEquipment((int)oldItem.equipSlot, 0); //adds naked/unarmed to anim slot
    }
}
