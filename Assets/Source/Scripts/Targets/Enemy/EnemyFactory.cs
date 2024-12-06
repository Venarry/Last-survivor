using System.Threading.Tasks;
using Assets;
using ObjectPool;
using UnityEngine;

namespace Targets.Enemy
{
    public class EnemyFactory : TargetFactory
    {
        private readonly float _attackDistance;

        public EnemyFactory(
            TargetType targetType,
            TargetsProvider<Target> targetsProvider,
            AssetsProvider assetsProvider,
            AudioSource audioSource,
            string assetKey,
            float attackDistance)
            : base(targetType, targetsProvider, assetsProvider, audioSource, assetKey)
        {
            _attackDistance = attackDistance;
        }

        public async Task<Enemy> Create(Target attackTarget, float health, float damage, Vector3 position, Quaternion rotation)
        {
            PoolSpawnResult<Target> poolSpawnResult = await Create(health, position, rotation);
            Enemy enemy = poolSpawnResult.Result.GetComponent<Enemy>();

            if (poolSpawnResult.IsInstantiatedObject == true)
            {
                enemy.InitEnemy(attackTarget, _attackDistance, damage);
            }
            else
            {
                enemy.ResetDamage(damage);
            }

            return enemy;
        }
    }
}