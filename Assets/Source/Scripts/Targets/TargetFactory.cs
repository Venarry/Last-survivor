using UnityEngine;

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