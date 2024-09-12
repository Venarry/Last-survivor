using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class SaveData
{
    public List<LootData> Loots = new();
    public int TotalLevels = 0;
    public List<Type> Upgrades = new() { typeof(SwordRoundAttackSkill) };

    public void SetLoot(LootType lootType, int count)
    {
        LootData loot = Loots.FirstOrDefault(c => c.LootType == lootType);

        if(loot == null)
        {
            Loots.Add(new(lootType, count));
        }
        else
        {
            loot.Count = count;
        }
    }

    public void SetUpgrades(Type[] upgrades)
    {
        
    }

    public void SetLevels(int count)
    {
        TotalLevels = count;
    }

    public override string ToString()
    {
        return JsonUtility.ToJson(this);
    }
}

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
