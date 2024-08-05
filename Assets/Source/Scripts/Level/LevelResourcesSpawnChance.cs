using System.Collections.Generic;
using UnityEngine;

public class LevelResourcesSpawnChance
{
    private readonly Dictionary<LootType, float> _lootsSpawnChance = new()
    {
        [LootType.Wood] = 30f,
        [LootType.Diamond] = 10f,
    };

    public bool TryGetSpawnAccess(LootType lootType)
    {
        float spawnChance = _lootsSpawnChance[lootType];
        int roll = Random.Range(0, 101);

        return spawnChance >= roll;
    }
}
