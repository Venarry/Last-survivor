using System.Threading.Tasks;
using UnityEngine;

public abstract class TargetFactory : ObjectPoolBehaviour<Target>
{
    private readonly TargetsProvider _targetsProvider;

    protected TargetFactory(TargetsProvider targetsProvider, AssetsProvider assetsProvider) : base(assetsProvider)
    {
        _targetsProvider = targetsProvider;
    }

    protected abstract TargetType TargetType { get; }

    public async Task<Target> Create(float health, Vector3 position, Quaternion rotation)
    {
        PoolResult<Target> poolResult = await CreatePoolObject(position, rotation);
        Target target = poolResult.Result;

        if (poolResult.IsInstantiatedObject == true)
        {
            HealthModel healthModel = new(health);
            target.Init(TargetType, healthModel);
            OnCreated(target, healthModel);
        }
        else
        {
            target.ResetSettings(health);
        }

        _targetsProvider.Add(target);
        target.LifeCycleEnded += OnLifeCycleEnd;

        return target;
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