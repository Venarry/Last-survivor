using System.Threading.Tasks;
using UnityEngine;

public class EnemyFactory : TargetFactory
{
    private readonly Target _attackTarget;
    private readonly float _attackDistance;

    public EnemyFactory(
        TargetsProvider targetsProvider,
        AssetsProvider assetsProvider,
        Target attackTarget,
        float attackDistance) : base(targetsProvider, assetsProvider)
    {
        _attackTarget = attackTarget;
        _attackDistance = attackDistance;
    }

    protected override string AssetKey => AssetsKeys.Enemy;
    protected override TargetType TargetType => TargetType.Enemy;

    protected override void OnCreated(Target target, HealthModel healthModel)
    {
        Enemy enemy = target.GetComponent<Enemy>();
        enemy.Init(_attackTarget, _attackDistance);
    }
}
