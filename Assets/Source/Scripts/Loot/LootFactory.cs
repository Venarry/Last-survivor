using System.Threading.Tasks;
using Assets;
using ObjectPool;
using Targets;
using UnityEngine;

namespace ObstacleLoot
{
    public abstract class LootFactory : ObjectPoolBehaviour<Loot>
    {
        private readonly ILootHolder _lootHolder;
        private readonly TargetsProvider<Loot> _targetProvider;
        protected AssetsProvider AssetsProvider;

        protected LootFactory(
            ILootHolder lootHolder,
            TargetsProvider<Loot> targetsProvider,
            AssetsProvider assetsProvider)
            : base(assetsProvider)
        {
            _lootHolder = lootHolder;
            _targetProvider = targetsProvider;
            AssetsProvider = assetsProvider;
        }

        protected virtual int BaseRewardCount { get; } = 1;
        protected virtual float BaseExperienceCount { get; } = 1;

        public async Task<Loot> Create(Vector3 position, int rewardMultiplier, float experienceMultiplier)
        {
            PoolSpawnResult<Loot> poolResult = await CreatePoolObject(position, Quaternion.identity);
            Loot loot = poolResult.Result;

            if (poolResult.IsInstantiatedObject == true)
            {
                loot.Init(BaseRewardCount * rewardMultiplier, BaseExperienceCount * experienceMultiplier, _lootHolder);
            }
            else
            {
                loot.ResetSettings(BaseRewardCount * rewardMultiplier, BaseExperienceCount * experienceMultiplier);
            }

            _targetProvider.Add(loot);

            loot.LifeCycleEnded += OnLootEnd;
            loot.GoToPlayer();

            return loot;
        }

        private void OnLootEnd(Loot loot)
        {
            loot.LifeCycleEnded -= OnLootEnd;
            _targetProvider.Remove(loot);
        }
    }
}