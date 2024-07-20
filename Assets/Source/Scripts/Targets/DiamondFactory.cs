using UnityEngine;

public class DiamondFactory
{
    private readonly Diamond _diamondPrefab = Resources.Load<Diamond>(ResourcesPath.Diamond);
    private readonly TargetsProvider _targetsProvider;
    private readonly LootFactory _lootFactory;

    public DiamondFactory(TargetsProvider targetsProvider, LootFactory lootFactory)
    {
        _targetsProvider = targetsProvider;
        _lootFactory = lootFactory;
    }

    public Diamond Create(Vector3 position, Quaternion rotation)
    {
        Diamond diamond = Object.Instantiate(_diamondPrefab, position, rotation);

        int maxHealth = 3;
        HealthModel healthModel = new(maxHealth);
        diamond.InitHealthView(healthModel);
        diamond.InitLootDropHandler(_lootFactory);

        _targetsProvider.Add(diamond);
        diamond.HealthOver += OnHealthOver;
        return diamond;
    }

    private void OnHealthOver(Target target)
    {
        _targetsProvider.Remove(target);
        target.HealthOver -= OnHealthOver;
    }
}
