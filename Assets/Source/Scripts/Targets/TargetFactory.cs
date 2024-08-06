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

    public virtual async Task<Target> Create(int health, Vector3 position, Quaternion rotation)
    {
        PoolResult<Target> poolResult = await CreatePoolObject(position, rotation);
        Target target = poolResult.Result;

        if (poolResult.IsInstantiatedObject == true)
        {
            HealthModel healthModel = new(health);
            target.Init(TargetType, healthModel);

            _targetsProvider.Add(target);
            target.HealthOver += OnHealthOver;
        }
        else
        {
            target.ResetSettings(health);
        }

        return target;
    }

    private void OnHealthOver(Target target)
    {
        _targetsProvider.Remove(target);
        target.HealthOver -= OnHealthOver;
    }
}