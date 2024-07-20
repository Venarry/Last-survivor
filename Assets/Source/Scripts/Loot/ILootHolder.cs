using UnityEngine;

public interface ILootHolder
{
    public Vector3 Position { get; }
    public void Add(LootType lootType, int count);
}
