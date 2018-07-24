using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerLoadout : DefaultEquipment {

    protected override void InstantiateDefaultEquipment()
    {
        base.InstantiateDefaultEquipment();
        defaultArmor = new Item[] { MasterItemList.ClothShirt(),
                                    MasterItemList.ClothTrousers(),
                                    MasterItemList.LeatherBoots(),
                                    MasterItemList.LeatherGloves()};
        Debug.Log("Instantiated armor " + defaultArmor.Length);

        defaultWeapon = new Item[0];
        
    }
}
