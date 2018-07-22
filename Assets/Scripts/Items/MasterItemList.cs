using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterItemList : MonoBehaviour {
    public static Item IronChest()
    {
        Item item = new Item();

        item.name = "Iron Body Armor";
        item.myDescription = "This is iron body armor covering the chest, abdominals, and both arms.";
        //item.icon = "fileName"

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
}
