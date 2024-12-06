using System.Threading.Tasks;
using Assets;
using ObjectPool;
using Targets;
using UnityEngine;

namespace ObstacleLoot
{
    public class LootFactory : ObjectPoolBehaviour<Loot>
    {
        private readonly ILootHolder _lootHolder;
        private readonly LootType _lootType;
        private readonly TargetsProvider<Loot> _targetProvider;
        private readonly string _assetKey;

        public LootFactory(
            ILootHolder lootHolder,
            LootType lootType,
            TargetsProvider<Loot> targetsProvider,
            string assetKey,
            AssetsProvider assetsProvider)
            : base(assetsProvider)
        {
            _lootHolder = lootHolder;
            _lootType = lootType;
            _targetProvider = targetsProvider;
            _assetKey = assetKey;
        }

        protected virtual int BaseRewardCount { get; } = 1;
        protected virtual float BaseExperienceCount { get; } = 1;
        protected override string AssetKey => _assetKey;

        public async Task<Loot> Create(Vector3 position, int rewardMultiplier, float experienceMultiplier)
        {
            PoolSpawnResult<Loot> poolResult = await CreatePoolObject(position, Quaternion.identity);
            Loot loot = poolResult.Result;

            if (poolResult.IsInstantiatedObject == true)
            {
                loot.Init(BaseRewardCount * rewardMultiplier, BaseExperienceCount * experienceMultiplier, _lootHolder, _lootType);
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