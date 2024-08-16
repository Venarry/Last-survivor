using System.Threading.Tasks;
using UnityEngine;

public class DiamondFactory : TargetWithLootFactory
{
    public DiamondFactory(
        LevelsStatisticModel levelsStatisticModel,
        TargetsProvider targetsProvider,
        AssetsProvider assetsProvider,
        DiamondLootFactory lootFactory) 
        : base(levelsStatisticModel, targetsProvider, assetsProvider, lootFactory)
    {
    }

    protected override string AssetKey => AssetsKeys.Diamond;
    protected override TargetType TargetType => TargetType.Ore;
}