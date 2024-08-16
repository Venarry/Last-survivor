using UnityEngine;

[RequireComponent(typeof(LootDropHandler))]
public class TargetWithLoot : Target
{
    private LootDropHandler _lootDropHandler;

    protected override void OnAwake()
    {
        _lootDropHandler = GetComponent<LootDropHandler>();
    }

    public void InitLootDropHandler(HealthModel healthModel, LootFactory lootFactory, LevelsStatisticModel levelsStatisticModel)
    {
        _lootDropHandler.Init(healthModel, lootFactory, levelsStatisticModel);
    }
}
