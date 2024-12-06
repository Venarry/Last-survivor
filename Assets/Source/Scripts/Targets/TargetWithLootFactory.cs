using Assets;
using Health;
using Level;
using ObstacleLoot;
using UnityEngine;

namespace Targets
{
    public class TargetWithLootFactory : TargetFactory
    {
        private readonly LevelsStatisticModel _levelsStatisticModel;
        private readonly LootFactory _lootFactory;

        public TargetWithLootFactory(
            TargetType targetType,
            LevelsStatisticModel levelsStatisticModel,
            TargetsProvider<Target> targetsProvider,
            AssetsProvider assetsProvider,
            AudioSource audioSource,
            LootFactory lootFactory,
            string assetKey)
            : base(targetType, targetsProvider, assetsProvider, audioSource, assetKey)
        {
            _levelsStatisticModel = levelsStatisticModel;
            _lootFactory = lootFactory;
        }

        protected override void OnCreated(Target target, HealthModel healthModel)
        {
            TargetWithLoot targetWithLoot = target as TargetWithLoot;
            targetWithLoot.InitLootDropHandler(healthModel, _lootFactory, _levelsStatisticModel);
        }
    }
}