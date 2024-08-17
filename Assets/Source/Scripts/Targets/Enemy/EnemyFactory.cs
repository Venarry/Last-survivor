using System.Threading.Tasks;
using UnityEngine;

public class EnemyFactory : TargetFactory
{
    private readonly float _attackDistance;

    public EnemyFactory(
        TargetsProvider targetsProvider,
        AssetsProvider assetsProvider,
        float attackDistance) : base(targetsProvider, assetsProvider)
    {
        _attackDistance = attackDistance;
    }

    protected override string AssetKey => AssetsKeys.Enemy;
    protected override TargetType TargetType => TargetType.Enemy;

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
