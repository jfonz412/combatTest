using System.Collections.Generic;
using UnityEngine;

public class MasterItemList : MonoBehaviour {
    public enum Items { Log, WoodAxe, IronPickaxe, Stone };

    //This is probably slower than calling the method to return the item directly, but it allows scripts like ItemPickup to use the enums to select an item
    //use the direct method where possible, like Shop/Loot lists, but this is here if we need it
    private static Dictionary<Items, Item> itemLookup = new Dictionary<Items, Item>()
    {
        { Items.Log, Log() },
        { Items.Stone, Stone() },
        { Items.WoodAxe, WoodAxe() },
        { Items.IronPickaxe, IronPickaxe() },
    };

    public static Item GetItem(Items requestedItem)
    {
        return new Item(itemLookup[requestedItem]);
    }

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
        item.Init();
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
        item.Init();
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
        item.Init();
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
        item.Init();
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
        item.Init();
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
        item.Init();
        return item;
    }
    public static Item IronHatchet()
    {
        Item item = new Item();

        item.name = "Iron Hatchet";
        item.myDescription = "Can be used for self defense as well as chopping wood";
        item.icon = "IronHatchet";
        item.myEquipSlot = Item.EquipmentSlot.MainHand;
        item.myMaterial = ItemMaterial.Material.Iron;
        item.weight = 2f;
        item.stackable = false;
        item.quantity = 1;
        item.maxQuantity = 1;
        item.myWeaponSkill = Item.WeaponSkill.Axe;
        item.myAttackType = Item.AttackType.Hack;
        item.myUseEffect = ItemEffects.ItemEffect.Equip;
        item.myToolType = Item.ToolType.Axe;
        item.Init();
        return item;
    }
    public static Item WoodAxe()
    {
        Item item = new Item();

        item.name = "Wood Axe";
        item.myDescription = "A lumberjack's favorite tool, also a devestating (yet unwieldly) weapon";
        item.icon = "WoodAxe";
        item.myEquipSlot = Item.EquipmentSlot.MainHand;
        item.myMaterial = ItemMaterial.Material.Iron;
        item.weight = 4f;
        item.stackable = false;
        item.quantity = 1;
        item.maxQuantity = 1;
        item.myWeaponSkill = Item.WeaponSkill.Axe;
        item.myAttackType = Item.AttackType.Hack;
        item.myUseEffect = ItemEffects.ItemEffect.Equip;
        item.myToolType = Item.ToolType.Axe;
        item.Init();
        return item;
    }
    public static Item IronPickaxe()
    {
        Item item = new Item();

        item.name = "Iron Pickaxe";
        item.myDescription = "Can be used to break rocks or skulls";
        item.icon = "IronPickaxe";
        item.myEquipSlot = Item.EquipmentSlot.MainHand;
        item.myMaterial = ItemMaterial.Material.Iron;
        item.weight = 2;
        item.stackable = false;
        item.quantity = 1;
        item.maxQuantity = 1;
        item.myWeaponSkill = Item.WeaponSkill.Pick;
        item.myAttackType = Item.AttackType.Stab;
        item.myUseEffect = ItemEffects.ItemEffect.Equip;
        item.myToolType = Item.ToolType.Pick;
        item.Init();
        return item;
    }
    public static Item WoodenShield()
    {
        Item item = new Item();

        item.name = "Wooden Shield";
        item.myDescription = "Will block most blows";
        item.icon = "WoodenShield";
        item.myEquipSlot = Item.EquipmentSlot.OffHand;
        item.myMaterial = ItemMaterial.Material.Wood;
        item.weight = 3f;
        item.stackable = false;
        item.quantity = 1;
        item.maxQuantity = 1;
        item.myWeaponSkill = Item.WeaponSkill.Offhand;
        item.myAttackType = Item.AttackType.BluntImpact;
        item.myUseEffect = ItemEffects.ItemEffect.Equip;
        item.myToolType = Item.ToolType.NA;
        item.Init();
        return item;
    }
    public static Item IronShield()
    {
        Item item = new Item();

        item.name = "Iron Shield";
        item.myDescription = "Will block most blows, but it's pretty heavy";
        item.icon = "IronShield";
        item.myEquipSlot = Item.EquipmentSlot.OffHand;
        item.myMaterial = ItemMaterial.Material.Iron;
        item.weight = 5f;
        item.stackable = false;
        item.quantity = 1;
        item.maxQuantity = 1;
        item.myWeaponSkill = Item.WeaponSkill.Offhand;
        item.myAttackType = Item.AttackType.BluntImpact;
        item.myUseEffect = ItemEffects.ItemEffect.Equip;
        item.myToolType = Item.ToolType.NA;
        item.Init();
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
        item.Init();
        return item;
    }
    public static Item Stone()
    {
        Item item = new Item();

        item.name = "Stone";
        item.myDescription = "Great for absolutely nothing right now";
        item.icon = "Stone";
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
        item.Init();
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
        item.Init();
        return item;
    }

    /*
     * Subpart items are the different elements a BodyPart to equip for offense or defense
     */
    public static Item Fist()
    {
        Item item = new Item();

        item.name = "fist";
        item.myDescription = "SUBPART ITEM, SHOULD NOT BE IN INVENTORY";
        //item.icon = "NO ICON"
        //item.myEquipSlot = ANY (both hands)
        item.myMaterial = ItemMaterial.Material.Bone;
        item.weight = 1f;
        item.stackable = false;
        item.quantity = 1;
        item.maxQuantity = 1;
        item.myAttackType = Item.AttackType.BluntImpact;
        item.myUseEffect = ItemEffects.ItemEffect.Equip;
        item.myToolType = Item.ToolType.NA;
        item.myWeaponSkill = Item.WeaponSkill.Hands;
        return item;
    }
    /* //not used
    public static Item BareFlesh()
    {
        Item item = new Item();

        item.name = "bare flesh";
        item.myDescription = "SUBPART ITEM, SHOULD NOT BE IN INVENTORY";
        //item.icon = "NO ICON"
        //item.myEquipSlot = ANY
        item.myMaterial = ItemMaterial.Material.NA;
        item.weight = 1f;
        item.stackable = false;
        item.quantity = 1;
        item.maxQuantity = 1;
        item.myAttackType = Item.AttackType.NA;
        item.myUseEffect = ItemEffects.ItemEffect.Equip;
        item.myToolType = Item.ToolType.NA;
        item.myWeaponSkill = Item.WeaponSkill.NA;
        return item;
    } */

    public static Item FurSkin()
    {
        Item item = new Item();

        item.name = "fur skin";
        item.myDescription = "SUBPART ITEM, SHOULD NOT BE IN INVENTORY";
        //item.icon = "NO ICON"
        //item.myEquipSlot = ANY
        item.myMaterial = ItemMaterial.Material.FurSkin;
        item.weight = 1f;
        item.stackable = false;
        item.quantity = 1;
        item.maxQuantity = 1;
        item.myAttackType = Item.AttackType.NA;
        item.myUseEffect = ItemEffects.ItemEffect.Equip;
        item.myToolType = Item.ToolType.NA;
        item.myWeaponSkill = Item.WeaponSkill.NA;
        return item;
    }

    public static Item ThichFurSkin()
    {
        Item item = new Item();

        item.name = "thich fur skin";
        item.myDescription = "SUBPART ITEM, SHOULD NOT BE IN INVENTORY";
        //item.icon = "NO ICON"
        //item.myEquipSlot = ANY
        item.myMaterial = ItemMaterial.Material.ThickFurSkin;
        item.weight = 1f;
        item.stackable = false;
        item.quantity = 1;
        item.maxQuantity = 1;
        item.myAttackType = Item.AttackType.NA;
        item.myUseEffect = ItemEffects.ItemEffect.Equip;
        item.myToolType = Item.ToolType.NA;
        item.myWeaponSkill = Item.WeaponSkill.NA;
        return item;
    }

    public static Item MainClaw()
    {
        Item item = new Item();

        item.name = "claw";
        item.myDescription = "SUBPART ITEM, SHOULD NOT BE IN INVENTORY";
        //item.icon = "NO ICON"
        item.myEquipSlot = Item.EquipmentSlot.MainHand;
        item.myMaterial = ItemMaterial.Material.Bone;
        item.weight = 1f;
        item.stackable = false;
        item.quantity = 1;
        item.maxQuantity = 1;
        item.myAttackType = Item.AttackType.Claw; 
        item.myUseEffect = ItemEffects.ItemEffect.Equip;
        item.myToolType = Item.ToolType.NA;
        item.myWeaponSkill = Item.WeaponSkill.Claws;
        return item;
    }
    public static Item MainTeeth()
    {
        Item item = new Item();

        item.name = "teeth";
        item.myDescription = "SUBPART ITEM, SHOULD NOT BE IN INVENTORY";
        //item.icon = "NO ICON"
        item.myEquipSlot = Item.EquipmentSlot.MainHand;
        item.myMaterial = ItemMaterial.Material.Bone;
        item.weight = 1f;
        item.stackable = false;
        item.quantity = 1;
        item.maxQuantity = 1;
        item.myAttackType = Item.AttackType.Bite;
        item.myUseEffect = ItemEffects.ItemEffect.Equip;
        item.myToolType = Item.ToolType.NA;
        item.myWeaponSkill = Item.WeaponSkill.Biting;
        return item;
    }

    public static Item OffClaw()
    {
        Item item = new Item();

        item.name = "claw";
        item.myDescription = "SUBPART ITEM, SHOULD NOT BE IN INVENTORY";
        //item.icon = "NO ICON"
        item.myEquipSlot = Item.EquipmentSlot.OffHand;
        item.myMaterial = ItemMaterial.Material.Bone;
        item.weight = 1f;
        item.stackable = false;
        item.quantity = 1;
        item.maxQuantity = 1;
        item.myAttackType = Item.AttackType.Claw;
        item.myUseEffect = ItemEffects.ItemEffect.Equip;
        item.myToolType = Item.ToolType.NA;
        item.myWeaponSkill = Item.WeaponSkill.Claws;
        return item;
    }
    public static Item OffTeeth()
    {
        Item item = new Item();

        item.name = "teeth";
        item.myDescription = "SUBPART ITEM, SHOULD NOT BE IN INVENTORY";
        //item.icon = "NO ICON"
        item.myEquipSlot = Item.EquipmentSlot.OffHand;
        item.myMaterial = ItemMaterial.Material.Bone;
        item.weight = 1f;
        item.stackable = false;
        item.quantity = 1;
        item.maxQuantity = 1;
        item.myAttackType = Item.AttackType.Bite;
        item.myUseEffect = ItemEffects.ItemEffect.Equip;
        item.myToolType = Item.ToolType.NA;
        item.myWeaponSkill = Item.WeaponSkill.Biting;
        return item;
    }
}
