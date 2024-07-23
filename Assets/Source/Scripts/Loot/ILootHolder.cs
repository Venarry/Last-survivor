using UnityEngine;

public interface ILootHolder
{
    public Vector3 ReceivingPosition { get; }
    public void Add(LootType lootType, int count);
    public void Add(int experience);
}
