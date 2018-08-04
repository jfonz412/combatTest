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
        item.myAttackType = Item.AttackType.NA;
        item.myUseEffect = ItemEffects.ItemEffect.Equip;
        item.myToolType = Item.ToolType.NA;
        item.myWeaponSkill = Item.WeaponSkill.NA;
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
        item.myWeaponSkill = Item.WeaponSkill.Hands;
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
        item.stackable = false;
        item.quantity = _quantity;
        item.maxQuantity = 1;
        item.myWeaponSkill = Item.WeaponSkill.NA; //should cancel below, need NA attacktype to be more explicit here
        item.myAttackType = Item.AttackType.NA; 
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
        item.stackable = false;
        item.quantity = 1;
        item.maxQuantity = 1;
        item.myWeaponSkill = Item.WeaponSkill.NA; 
        item.myAttackType = Item.AttackType.NA; 
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
        item.stackable = false;
        item.quantity = 1;
        item.maxQuantity = 1;
        item.myWeaponSkill = Item.WeaponSkill.NA; 
        item.myAttackType = Item.AttackType.NA; 
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
        item.stackable = false;
        item.quantity = 1;
        item.maxQuantity = 1;
        item.myWeaponSkill = Item.WeaponSkill.NA;
        item.myAttackType = Item.AttackType.NA;
        item.myUseEffect = ItemEffects.ItemEffect.Equip;
        item.myToolType = Item.ToolType.NA;
        return item;
    }
    public static Item IronDagger()
    {
        Item item = new Item();

        item.name = "Iron Dagger";
        item.myDescription = "Small but effective";
        item.icon = "IronDagger";
        item.myEquipSlot = Item.EquipmentSlot.MainHand;
        item.myMaterial = ItemMaterial.Material.Iron;
        item.weight = 1f;
        item.stackable = false;
        item.quantity = 1;
        item.maxQuantity = 1;
        item.myWeaponSkill = Item.WeaponSkill.Dagger;
        item.myAttackType = Item.AttackType.Stab;
        item.myUseEffect = ItemEffects.ItemEffect.Equip;
        item.myToolType = Item.ToolType.NA;
        return item;
    }

    public static Item Log()
    {
        Item item = new Item();

        item.name = "Log";
        item.myDescription = "It's big, it's heavy, it's wood.";
        item.icon = "Log";
        item.myEquipSlot = Item.EquipmentSlot.NA;
        item.myMaterial = ItemMaterial.Material.NA;
        item.weight = 5f;
        item.stackable = true;
        item.quantity = 1;
        item.maxQuantity = 3;
        item.myWeaponSkill = Item.WeaponSkill.NA;
        item.myAttackType = Item.AttackType.NA;
        item.myUseEffect = ItemEffects.ItemEffect.NA;
        item.myToolType = Item.ToolType.NA;
        return item;
    }
    public static Item HealthPotion()
    {
        Item item = new Item();

        item.name = "Health Potion";
        item.myDescription = "A mysterious potion that can heal even the gravest of wounds...but at what cost?";
        item.icon = "HealthPotion";
        item.myEquipSlot = Item.EquipmentSlot.NA;
        item.myMaterial = ItemMaterial.Material.NA;
        item.weight = 0.5f;
        item.stackable = true;
        item.quantity = 1;
        item.maxQuantity = 3;
        item.myWeaponSkill = Item.WeaponSkill.NA;
        item.myAttackType = Item.AttackType.NA;
        item.myUseEffect = ItemEffects.ItemEffect.HealthPotion;
        item.myToolType = Item.ToolType.NA;
        item.baseValue = 250f;
        item.destroyOnUse = true;
        return item;
    }
}
