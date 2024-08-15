using System;
using System.Collections.Generic;

public class InventoryModel
{
    private readonly Dictionary<LootType, int> _loot = new();

    public event Action<LootType, int> ItemAdded;

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

        ItemAdded?.Invoke(lootType, _loot[lootType]);
    }

    public bool TryRemove(Dictionary<LootType, int> items)
    {
        if (HasItems(items) == false)
        {
            return false;
        }

        foreach (KeyValuePair<LootType, int> item in items)
        {
            _loot[item.Key] -= item.Value;

            if (_loot[item.Key] == 0)
            {
                _loot.Remove(item.Key);
            }
        }
        
        return true;
    }

    private bool HasItems(Dictionary<LootType, int> items)
    {
        foreach (KeyValuePair<LootType, int> item in items)
        {
            if(_loot.ContainsKey(item.Key) == false)
            {
                return false;
            }

            if (_loot[item.Key] < item.Value)
            {
                return false;
            }
        }

        return true;
    }
}
