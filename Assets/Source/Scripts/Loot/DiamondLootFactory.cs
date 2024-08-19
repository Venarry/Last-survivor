public class DiamondLootFactory : LootFactory
{
    public DiamondLootFactory(ILootHolder lootHolder, AssetsProvider assetsProvider) : base(lootHolder, assetsProvider)
    {
    }

    protected override string AssetKey => AssetsKeys.DiamondLoot;
    protected override int BaseExperienceCount => 2;
}
