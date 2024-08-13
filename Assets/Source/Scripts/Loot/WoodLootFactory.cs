public class WoodLootFactory : LootFactory
{
    public WoodLootFactory(ILootHolder lootHolder, AssetsProvider assetsProvider) : base(lootHolder, assetsProvider)
    {
    }

    protected override string AssetKey => AssetsKeys.WoodLoot;
}
