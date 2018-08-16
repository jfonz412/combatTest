public class BearLoadout : DefaultEquipment
{
    protected override void InstantiateDefaultEquipment()
    {
        base.InstantiateDefaultEquipment();
        defaultArmor = new Item[] { MasterItemList.ThichFurSkin() };

        defaultWeapon = new Item[] { MasterItemList.MainClaw(),
                                     MasterItemList.OffTeeth() };
    }
}
