using UnityEngine;

public abstract class LootFactory
{
    protected abstract Loot Prefab { get; }

    public Loot Create(Vector3 position, int reward, LootType lootType)
    {
        Loot loot = Object.Instantiate(Prefab, position, Quaternion.identity);
        loot.Init(reward, lootType);
        
        return loot;
    }
}
