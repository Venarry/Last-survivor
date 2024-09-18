public class WoodLootFactory : LootFactory
{
    public WoodLootFactory(ILootHolder lootHolder, TargetsProvider<Loot> lootProvider, AssetsProvider assetsProvider) : base(lootHolder, lootProvider, assetsProvider)
    {
    }

    protected override string AssetKey => AssetsKeys.WoodLoot;
}
