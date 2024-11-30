using Assets;
using Configs;
using Level;
using ObstacleLoot;
using UnityEngine;

namespace Targets
{
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
}