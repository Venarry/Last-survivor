public class WoodFactory : TargetWithLootFactory
{
    public WoodFactory(
        LevelsStatisticModel levelsStatisticModel,
        TargetsProvider<Target> targetsProvider,
        AssetsProvider assetsProvider,
        WoodLootFactory lootFactory) 
        : base(levelsStatisticModel, targetsProvider, assetsProvider, lootFactory)
    {
    }

    protected override string AssetKey => AssetsKeys.Wood;
    protected override TargetType TargetType => TargetType.Wood;
}
