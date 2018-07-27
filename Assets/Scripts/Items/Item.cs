using System;
using UnityEngine;

[Serializable]
public class Item {
    
    public Item(Item item = null)
    {
    }
    
    
    //[HideInInspector]
    //public Transform user; can't serialize with this

    public ItemMaterial.Material myMaterial;
    public ItemEffects.ItemEffect myUseEffect;

    public string icon = null;
    public int? mySlotNum; //allows the int to be null

    public string name = "New Item";
    public string myDescription;

    public float baseValue = 100f;
    public float currentValue;

    public int quantity = 1;
    public int maxQuantity = 99;
    public bool stackable;

    public float weight;
    public float hardnessValue;

    //durability
    //condition
    //penetration?

    public enum WeaponType { Dagger, Spear, Axe, Pick, Hands, Offhand, Misc, NA }; //each of these is tied to a combat skill
    public enum AttackType { Hack, Stab, Projectile, BluntImpact, Punch, Claw, Bite };
    public enum EquipmentSlot { Head, Chest, Hands, Legs, MainHand, OffHand, Feet, NA }
    public enum ToolType { Pick, Axe, NA };

    public ToolType myToolType;
    public AttackType myAttackType;
    public EquipmentSlot myEquipSlot;
    public WeaponType myWeaponType;

    private void Start()
    {
        hardnessValue = ItemMaterial.HardnessValue(myMaterial);   
    }

    public virtual void Use() //RMB
    {
        //user = ScriptToolbox.GetInstance().GetPlayerManager().player.transform;
        ItemEffects.Use(myUseEffect);
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
        InventoryManager.GetInstance().GetInventory().RemoveAndDestroy(this);
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

