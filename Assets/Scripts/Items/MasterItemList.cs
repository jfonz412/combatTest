using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterItemList : MonoBehaviour {
    public static Item IronChest()
    {
        Item item = new Item();

        item.name = "Iron Body Armor";
        item.myDescription = "This is iron body armor covering the chest, abdominals, and both arms.";
        item.icon = "PlateIronChest";
        item.myEquipSlot = Item.EquipmentSlot.Chest;
        item.myMaterial = ItemMaterial.Material.Iron;
        item.weight = 10f;
        item.stackable = false;
        item.quantity = 1;
        item.maxQuantity = 1;
        item.myAttackType = Item.AttackType.BluntImpact;
        item.myUseEffect = ItemEffects.ItemEffect.Equip;
        item.myToolType = Item.ToolType.NA;
        item.myWeaponType = Item.WeaponType.Misc;
        return item;
    }
    public static Item Fist()
    {
        Item item = new Item();

        item.name = "Fist";
        item.myDescription = "WEAPON ITEM, SHOULD NOT BE IN INVENTORY";
        //item.icon = "fileName"
        //item.myEquipSlot = Item.EquipmentSlot. don't know if I need this for weapons?
        item.myMaterial = ItemMaterial.Material.NA;
        item.weight = 1f;
        item.stackable = false;
        item.quantity = 1;
        item.maxQuantity = 1;
        item.myAttackType = Item.AttackType.BluntImpact; //can be punch?
        item.myUseEffect = ItemEffects.ItemEffect.Equip;
        item.myToolType = Item.ToolType.NA;
        item.myWeaponType = Item.WeaponType.Hands;
        return item;
    }
    public static Item ClothShirt(int _quantity = 1)
    {
        Item item = new Item();

        item.name = "Cloth Shirt";
        item.myDescription = "Your average cloth shirt.";
        item.icon = "ClothShirt";
        item.myEquipSlot = Item.EquipmentSlot.Chest;
        item.myMaterial = ItemMaterial.Material.Cloth;
        item.weight = 0.8f;
        item.stackable = true;
        item.quantity = _quantity;
        item.maxQuantity = 3;
        item.myWeaponType = Item.WeaponType.NA; //should cancel below, need NA attacktype to be more explicit here
        item.myAttackType = Item.AttackType.BluntImpact; 
        item.myUseEffect = ItemEffects.ItemEffect.Equip;
        item.myToolType = Item.ToolType.NA;
        return item;
    }
    public static Item ClothTrousers()
    {
        Item item = new Item();

        item.name = "Cloth Trousers";
        item.myDescription = "Your average cloth trousers.";
        item.icon = "ClothTrousers";
        item.myEquipSlot = Item.EquipmentSlot.Legs;
        item.myMaterial = ItemMaterial.Material.Cloth;
        item.weight = 1f;
        item.stackable = true;
        item.quantity = 1;
        item.maxQuantity = 3;
        item.myWeaponType = Item.WeaponType.NA; 
        item.myAttackType = Item.AttackType.BluntImpact; 
        item.myUseEffect = ItemEffects.ItemEffect.Equip;
        item.myToolType = Item.ToolType.NA;
        return item;
    }
    public static Item LeatherBoots()
    {
        Item item = new Item();

        item.name = "Leather Boots";
        item.myDescription = "Leather boots for workin', walkin' and anything inbetween.";
        item.icon = "LeatherBoots";
        item.myEquipSlot = Item.EquipmentSlot.Feet;
        item.myMaterial = ItemMaterial.Material.Leather;
        item.weight = 3f;
        item.stackable = true;
        item.quantity = 1;
        item.maxQuantity = 2;
        item.myWeaponType = Item.WeaponType.Misc; 
        item.myAttackType = Item.AttackType.BluntImpact; 
        item.myUseEffect = ItemEffects.ItemEffect.Equip;
        item.myToolType = Item.ToolType.NA;
        return item;
    }
    public static Item LeatherGloves()
    {
        Item item = new Item();

        item.name = "Leather Gloves";
        item.myDescription = "Leather gloves offer decent protection from the elements.";
        item.icon = "LeatherGloves";
        item.myEquipSlot = Item.EquipmentSlot.Hands;
        item.myMaterial = ItemMaterial.Material.Leather;
        item.weight = 0.1f;
        item.stackable = true;
        item.quantity = 1;
        item.maxQuantity = 4;
        item.myWeaponType = Item.WeaponType.Misc;
        item.myAttackType = Item.AttackType.BluntImpact;
        item.myUseEffect = ItemEffects.ItemEffect.Equip;
        item.myToolType = Item.ToolType.NA;
        return item;
    }   
}
