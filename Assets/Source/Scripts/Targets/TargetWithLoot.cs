using UnityEngine;

[RequireComponent(typeof(LootDropHandler))]
public class TargetWithLoot : Target
{
    [SerializeField] private LootDropHandler _lootDropHandler;

    public void InitLootDropHandler(LootFactory lootFactory)
    {
        _lootDropHandler.Init(lootFactory);
    }
}
