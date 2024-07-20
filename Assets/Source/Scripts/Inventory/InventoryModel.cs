using System.Collections.Generic;

public class InventoryModel
{
    private readonly Dictionary<LootType, int> _loot = new();

    public void Add(LootType lootType, int count)
    {
        if (_loot.ContainsKey(lootType) == false)
        {
            _loot.Add(lootType, count);
        }
        else
        {
            _loot[lootType] += count;
        }
    }
}
