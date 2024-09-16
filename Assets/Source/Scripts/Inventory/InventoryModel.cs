using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryModel
{
    private readonly Dictionary<LootType, int> _loot = new();

    public event Action<LootType, int> ItemChanged;

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

        ItemChanged?.Invoke(lootType, _loot[lootType]);
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

            ItemChanged?.Invoke(item.Key, _loot[item.Key]);

            if (_loot[item.Key] == 0)
            {
                _loot.Remove(item.Key);
            }
        }
        
        return true;
    }

    public Dictionary<LootType, int> GetAll() => _loot.ToDictionary(c => c.Key, c => c.Value);

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
