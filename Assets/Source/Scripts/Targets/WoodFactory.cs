public class WoodFactory : TargetWithLootFactory
{
    public WoodFactory(
        TargetsProvider targetsProvider,
        AssetsProvider assetsProvider,
        WoodLootFactory lootFactory) 
        : base(targetsProvider, assetsProvider, lootFactory)
    {
    }

    protected override string AssetKey => AssetsKeys.Wood;
    protected override TargetType TargetType => TargetType.Wood;
}
