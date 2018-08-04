using System;
using UnityEngine;

[Serializable]
public class Item {
    
    public Item(Item _item = null)
    {
        if (_item == null)
            return;

        myMaterial = _item.myMaterial;
        myUseEffect = _item.myUseEffect;

        icon = _item.icon;
        mySlotNum = _item.mySlotNum; //allows the to be null

        name = _item.name;
        myDescription = _item.myDescription;

        baseValue = _item.baseValue;
        currentValue = _item.currentValue;

        quantity = _item.quantity;
        maxQuantity = _item.maxQuantity;
        stackable = _item.stackable;

        weight = _item.weight;
        hardnessValue = _item.hardnessValue;

        destroyOnUse = _item.destroyOnUse;

    //durability
    //condition
    //penetration?

    myToolType = _item.myToolType;
        myAttackType = _item.myAttackType;
        myEquipSlot = _item.myEquipSlot;
        myWeaponSkill = _item.myWeaponSkill;
    }
    
    public ItemMaterial.Material myMaterial; //used to calculate hardness of an object
    public ItemEffects.ItemEffect myUseEffect; //what happens when player right clicks this item in inventory

    public string icon = ""; //file path to image used for item icon
    public int? mySlotNum; //?allows the int to be null, used to save inventory spot

    public string name;
    public string myDescription;

    public float baseValue = 0f; //base value of this item before economy modifiers
    public float currentValue;   //value after mods

    public int quantity = 1;
    public int maxQuantity = 99;
    public bool stackable;

    public float weight;
    public float hardnessValue;

    public bool destroyOnUse { get; set; } //not sure why but I need this in order to set bool in new Item

    //durability
    //condition
    //penetration?

    public enum WeaponSkill { Dagger, Spear, Axe, Pick, Hands, Offhand, Misc, NA }; //each of these is tied to a combat skill and used to see if weildable
    public enum AttackType { Hack, Stab, Projectile, BluntImpact, Punch, Claw, Bite, NA }; //used to determine injury
    public enum EquipmentSlot { Head, Chest, Hands, Legs, MainHand, OffHand, Feet, NA } //used to determin slot in equipment manager
    public enum ToolType { Pick, Axe, NA };

    public ToolType myToolType;
    public AttackType myAttackType;
    public EquipmentSlot myEquipSlot;
    public WeaponSkill myWeaponSkill;

    private void Start()
    {
        hardnessValue = ItemMaterial.HardnessValue(myMaterial);   
    }

    public virtual void Use() //RMB
    {
        //user = ScriptToolbox.GetInstance().GetPlayerManager().player.transform;
        ItemEffects.Use(myUseEffect, this);

        if (destroyOnUse)
        {
            quantity--;
            if(quantity == 0)
            {
                RemoveFromInventory();
            }
        }
    }

    public virtual void OpenStatWindow(string itemLocation)
    {
        DetermineValue(itemLocation);
        InfoPanel.instance.InstantiatePanel(); 
        ItemMenu.instance.PopulateInfo(PackageItemInfo()); 
        
    }

    //for stats
    protected virtual string[] PackageItemInfo()
    {
        string[] myStats = new string[4];
        if (this != null)
        {
            if (stackable)
            {
                myStats[0] = name + " (" + quantity.ToString() + ")";
            }
            else
            {
                myStats[0] = name;
            }
            myStats[1] = "Value: " + currentValue.ToString();
            myStats[3] = myDescription;
        }
        return myStats;
    }

    private void RemoveFromInventory()
    {
        InventoryManager.GetInstance().GetInventory().Remove(this);
    }

    public void DetermineValue(string itemLocation)
    {
        if (itemLocation == "Shop")
        {
            currentValue = PriceChecker.AppraiseItem(this, "Purchase");
        }
        else
        {
            currentValue = PriceChecker.AppraiseItem(this, "Sale");
        }
    }
}

