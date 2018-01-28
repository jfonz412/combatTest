using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item {

    //video makes this a singleton, I am not doing that because I may want to use this script on
    //my npcs, or consider created an NPCEquipmentManager...

    public EquipmentSlot equipSlot;
    
    public override void Use()
    {
        base.Use();
        player.GetComponent<EquipmentManager>().Equip(this);
        RemoveFromInventory();
    }
}

public enum EquipmentSlot {Head, Chest, Legs, MainHand, OffHand, Feet}