using UnityEngine;

[RequireComponent(typeof(LootDropHandler))]
public class Diamond : Target
{
    private LootDropHandler _lootDropHandler;
    public override TargetType TargetType => TargetType.Ore;

    private void Awake()
    {
        _lootDropHandler = GetComponent<LootDropHandler>();
    }

    public void InitLootDropHandler(LootFactory lootFactory)
    {
        _lootDropHandler.Init(lootFactory);
    }
}