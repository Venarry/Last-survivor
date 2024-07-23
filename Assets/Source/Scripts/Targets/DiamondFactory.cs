using UnityEngine;

public class DiamondFactory : TargetFactory
{
    private readonly DiamondLootFactory _lootFactory;

    public DiamondFactory(
        TargetsProvider targetsProvider,
        DiamondLootFactory lootFactory) 
        : base(targetsProvider)
    {
        _lootFactory = lootFactory;
    }

    protected override Target Prefab => Resources.Load<Diamond>(ResourcesPath.Diamond);
    protected override TargetType TargetType => TargetType.Ore;

    public override Target Create(Vector3 position, Quaternion rotation)
    {
        Target target = base.Create(position, rotation);

        Diamond diamond = target as Diamond;
        diamond.InitLootDropHandler(_lootFactory);

        return target;
    }
}

public class WoodFactory : TargetFactory
{
    public WoodFactory(TargetsProvider targetsProvider) : base(targetsProvider)
    {
    }

    protected override Target Prefab => Resources.Load<Diamond>(ResourcesPath.Wood);
    protected override TargetType TargetType => TargetType.Wood;
}

public class EnemyFactory : TargetFactory
{
    public EnemyFactory(TargetsProvider targetsProvider) : base(targetsProvider)
    {
    }

    protected override Target Prefab => Resources.Load<Diamond>(ResourcesPath.Enemy);
    protected override TargetType TargetType => TargetType.Enemy;
}

public abstract class TargetFactory
{
    private readonly TargetsProvider _targetsProvider;

    protected TargetFactory(TargetsProvider targetsProvider)
    {
        _targetsProvider = targetsProvider;
    }

    protected abstract Target Prefab { get; }
    protected abstract TargetType TargetType { get; }

    public virtual Target Create(Vector3 position, Quaternion rotation)
    {
        Target target = Object.Instantiate(Prefab, position, rotation);

        int maxHealth = 3;
        HealthModel healthModel = new(maxHealth);
        target.Init(TargetType, healthModel);

        _targetsProvider.Add(target);
        target.HealthOver += OnHealthOver;

        return target;
    }

    private void OnHealthOver(Target target)
    {
        _targetsProvider.Remove(target);
        target.HealthOver -= OnHealthOver;
    }
}
