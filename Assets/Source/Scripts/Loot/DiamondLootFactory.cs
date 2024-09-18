public class DiamondLootFactory : LootFactory
{
    public DiamondLootFactory(ILootHolder lootHolder, TargetsProvider<Loot> lootProvider, AssetsProvider assetsProvider) 
        : base(lootHolder, lootProvider, assetsProvider)
    {
    }

    protected override string AssetKey => AssetsKeys.DiamondLoot;
    protected override float BaseExperienceCount => 5;
}
