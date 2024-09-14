using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class ProgressData
{
    public List<LootData> Loots = new();
    public List<UpgradeData> Upgrades = new();
    public List<UpgradeData> Skills = new();

    public int TotalLevels = 0;

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

    public void AddUpgrade(UpgradeType upgradeType, int level)
    {
        UpgradeData upgradeData = Upgrades.FirstOrDefault(c => c.Type == upgradeType);

        if(upgradeData == null)
            Upgrades.Add(new(upgradeType, level));
        else
            upgradeData.Level = level;
    }

    public void ResetUpgrades()
    {
        Upgrades.Clear();
    }

    public void AddSkill(UpgradeType upgradeType, int level)
    {
        UpgradeData upgradeData = Skills.FirstOrDefault(c => c.Type == upgradeType);

        if (upgradeData == null)
            Skills.Add(new(upgradeType, level));
        else
            upgradeData.Level = level;
    }

    public void ResetSkills()
    {
        Skills.Clear();
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
