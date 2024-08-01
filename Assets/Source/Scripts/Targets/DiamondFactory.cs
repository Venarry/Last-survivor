using System.Threading.Tasks;
using UnityEngine;

public class DiamondFactory : TargetWithLootFactory
{
    public DiamondFactory(
        TargetsProvider targetsProvider,
        AssetsProvider assetsProvider,
        DiamondLootFactory lootFactory) 
        : base(targetsProvider, assetsProvider, lootFactory)
    {
    }

    protected override string AssetKey => AssetsKeys.Diamond;
    protected override TargetType TargetType => TargetType.Ore;
}