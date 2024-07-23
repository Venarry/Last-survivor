using UnityEngine;

public abstract class LootFactory
{
    protected abstract Loot Prefab { get; }
    protected abstract LootType LootType { get; }
    private readonly ILootHolder _lootHolder;

    protected LootFactory(ILootHolder lootHolder)
    {
        _lootHolder = lootHolder;
    }

    public Loot Create(Vector3 position)
    {
        Loot loot = Object.Instantiate(Prefab, position, Quaternion.identity);

        int reward = 1;
        int experience = 1;
        loot.Init(reward, experience, LootType, _lootHolder);
        
        return loot;
    }
}