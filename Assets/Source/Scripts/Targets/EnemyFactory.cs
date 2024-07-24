using UnityEngine;

public class EnemyFactory : TargetFactory
{
    public EnemyFactory(TargetsProvider targetsProvider) : base(targetsProvider)
    {
    }

    protected override Target Prefab => Resources.Load<Target>(ResourcesPath.Enemy);
    protected override TargetType TargetType => TargetType.Enemy;

    public Target Create(int health, Vector3 position, Quaternion rotation, Target attackTarget, float attackDistance)
    {
        Target target = base.Create(health, position, rotation);
        Enemy enemy = target.GetComponent<Enemy>();
        enemy.Init(attackTarget, attackDistance);

        return target;
    }
}
