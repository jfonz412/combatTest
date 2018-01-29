using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item {

    public EquipmentSlot equipSlot;
    
    public override void Use()
    {
        base.Use(); //gets reference to the player
        player.GetComponent<EquipmentManager>().Equip(this);
        RemoveFromInventory();
    }
}

public enum EquipmentSlot {Head, Chest, Legs, MainHand, OffHand, Feet}