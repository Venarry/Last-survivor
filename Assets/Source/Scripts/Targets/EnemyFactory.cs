using System.Threading.Tasks;
using UnityEngine;

public class EnemyFactory : TargetFactory
{
    public EnemyFactory(TargetsProvider targetsProvider, AssetsProvider assetsProvider) : base(targetsProvider, assetsProvider)
    {
    }

    protected override string AssetKey => AssetsKeys.Enemy;
    protected override TargetType TargetType => TargetType.Enemy;

    public async Task<Target> Create(int health, Vector3 position, Quaternion rotation, Target attackTarget, float attackDistance)
    {
        Target target = await base.Create(health, position, rotation);
        Enemy enemy = target.GetComponent<Enemy>();
        enemy.Init(attackTarget, attackDistance);

        return target;
    }
}
