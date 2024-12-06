using System.Threading.Tasks;
using Assets;
using Buffs;
using Health;
using ObjectPool;
using UnityEngine;

namespace Targets
{
    public class TargetFactory : ObjectPoolBehaviour<Target>
    {
        private readonly TargetsProvider<Target> _targetsProvider;
        private readonly AudioSource _audioSource;
        private readonly string _assetKey;

        public TargetFactory(
            TargetType targetType,
            TargetsProvider<Target> targetsProvider,
            AssetsProvider assetsProvider,
            AudioSource audioSource,
            string assetKey)
            : base(assetsProvider)
        {
            TargetType = targetType;
            _targetsProvider = targetsProvider;
            _audioSource = audioSource;
            _assetKey = assetKey;
        }

        protected TargetType TargetType { get; private set; }
        protected override string AssetKey => _assetKey;

        public async Task<PoolSpawnResult<Target>> Create(float health, Vector3 position, Quaternion rotation)
        {
            PoolSpawnResult<Target> poolSpawnResult = await CreatePoolObject(position, rotation);
            Target target = poolSpawnResult.Result;

            if (poolSpawnResult.IsInstantiatedObject == true)
            {
                CharacterBuffsModel characterBuffsModel = new ();
                HealthModel healthModel = new (characterBuffsModel, health);
                target.Init(TargetType, healthModel);
                target.GetComponent<MapObstacleHitView>().Init(healthModel, _audioSource);
                OnCreated(target, healthModel);
            }
            else
            {
                target.ResetSettings(health);
            }

            _targetsProvider.Add(target);
            target.LifeCycleEnded += OnLifeCycleEnd;

            return poolSpawnResult;
        }

        protected virtual void OnCreated(Target target, HealthModel healthModel)
        {
        }

        private void OnLifeCycleEnd(Target target)
        {
            _targetsProvider.Remove(target);
            target.LifeCycleEnded -= OnLifeCycleEnd;
        }
    }
}