using System.Threading.Tasks;
using UnityEngine;

public abstract class TargetFactory
{
    private readonly AssetsProvider _assetsProvider;
    private readonly TargetsProvider _targetsProvider;

    protected TargetFactory(TargetsProvider targetsProvider, AssetsProvider assetsProvider)
    {
        _targetsProvider = targetsProvider;
        _assetsProvider = assetsProvider;
    }

    protected abstract string AssetKey { get; }
    protected abstract TargetType TargetType { get; }

    public virtual async Task<Target> Create(int health, Vector3 position, Quaternion rotation)
    {
        Target target = Object.Instantiate(await _assetsProvider.LoadGameObject<Target>(AssetKey), position, rotation);

        HealthModel healthModel = new(health);
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