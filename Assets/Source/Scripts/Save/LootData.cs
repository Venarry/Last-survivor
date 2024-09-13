using System;

[Serializable]
public class LootData
{
    public LootType LootType;
    public int Count;

    public LootData(LootType lootType, int count)
    {
        LootType = lootType;
        Count = count;
    }
}
