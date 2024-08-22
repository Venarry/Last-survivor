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

    public async Task<PoolSpawnResult<Target>> Create(float health, Vector3 position, Quaternion rotation)
    {
        PoolSpawnResult<Target> poolSpawnResult = await CreatePoolObject(position, rotation);
        Target target = poolSpawnResult.Result;

        if (poolSpawnResult.IsInstantiatedObject == true)
        {
            CharacterBuffsModel characterBuffsModel = new();
            HealthModel healthModel = new(characterBuffsModel, health);
            target.Init(TargetType, healthModel);
            OnCreated(target, healthModel);
        }
        else
        {
            target.ResetSettings(health);
        }

        _targetsProvider.Add(target);
        target.LifeCycleEnded += OnLifeCycleEnd;

        return poolSpawnResult;
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