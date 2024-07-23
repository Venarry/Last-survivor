using UnityEngine;

public class EnemyFactory : TargetFactory
{
    public EnemyFactory(TargetsProvider targetsProvider) : base(targetsProvider)
    {
    }

    protected override Target Prefab => Resources.Load<Target>(ResourcesPath.Enemy);
    protected override TargetType TargetType => TargetType.Enemy;
}
