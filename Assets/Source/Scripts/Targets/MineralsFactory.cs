using UnityEngine;

public class MineralsFactory
{
    private readonly Diamond _diamondPrefab = Resources.Load<Diamond>(ResourcesPath.Diamond);
    private readonly TargetsProvider _targetsProvider;

    public MineralsFactory(TargetsProvider targetsProvider)
    {
        _targetsProvider = targetsProvider;
    }

    public Diamond Create(Vector3 position, Quaternion rotation)
    {
        Diamond diamond = Object.Instantiate(_diamondPrefab, position, rotation);

        _targetsProvider.Add(diamond);
        return diamond;
    }
}
