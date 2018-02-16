﻿using UnityEngine;

//[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item {

    public EquipmentSlot equipSlot;
    public int equipmentID; //it's ID for the resources folder

    public override void Use()
    {
        base.Use(); //gets reference to the player

        //OpenStatWindow();
        Debug.Log("Displaying equipment stat window");

        //equiping should be done via mouseSlot swapping...NOT Use(), which will be a right click and will just open the stat menu
        player.GetComponent<EquipmentManager>().Equip(this); 
        RemoveFromInventory();
    }

}

public enum EquipmentSlot {Head, Chest, Legs, MainHand, OffHand, Feet}