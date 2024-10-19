using UnityEngine;

public class DiamondFactory : TargetWithLootFactory
{
    public DiamondFactory(
        LevelsStatisticModel levelsStatisticModel,
        TargetsProvider<Target> targetsProvider,
        AssetsProvider assetsProvider,
        AudioSource audioSource,
        DiamondLootFactory lootFactory) 
        : base(levelsStatisticModel, targetsProvider, assetsProvider, audioSource, lootFactory)
    {
    }

    protected override string AssetKey => AssetsKeys.Diamond;
    protected override TargetType TargetType => TargetType.Ore;
}