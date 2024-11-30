using Assets;
using Configs;
using Level;
using ObstacleLoot;
using UnityEngine;

namespace Targets
{
    public class WoodFactory : TargetWithLootFactory
    {
        public WoodFactory(
            LevelsStatisticModel levelsStatisticModel,
            TargetsProvider<Target> targetsProvider,
            AssetsProvider assetsProvider,
            AudioSource audioSource,
            WoodLootFactory lootFactory)
            : base(levelsStatisticModel, targetsProvider, assetsProvider, audioSource, lootFactory)
        {
        }

        protected override string AssetKey => AssetsKeys.Wood;
        protected override TargetType TargetType => TargetType.Wood;
    }
}