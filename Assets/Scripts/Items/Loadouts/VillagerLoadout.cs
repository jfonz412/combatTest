public class VillagerLoadout : DefaultEquipment
{
    protected override void InstantiateDefaultEquipment()
    {
        base.InstantiateDefaultEquipment();
        defaultArmor = new Item[] { MasterItemList.ClothShirt(),
                                    MasterItemList.ClothTrousers(),
                                    MasterItemList.LeatherBoots(),
                                    MasterItemList.LeatherGloves()};

        defaultWeapon = new Item[] { MasterItemList.IronDagger() };       
    }
}
