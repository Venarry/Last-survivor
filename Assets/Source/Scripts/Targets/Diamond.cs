using UnityEngine;

[RequireComponent(typeof(LootDropHandler))]
public class Diamond : Target
{
    private LootDropHandler _lootDropHandler;

    private void Awake()
    {
        _lootDropHandler = GetComponent<LootDropHandler>();
    }

    public void InitLootDropHandler(DiamondLootFactory lootFactory)
    {
        _lootDropHandler.Init(lootFactory);
    }
}