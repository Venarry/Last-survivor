using System;
using System.Collections.Generic;
using System.Linq;

public class PricesDataSource
{
    private readonly Dictionary<Type, Dictionary<LootType, int>> _baseUpgradesPrice = new()
    {
        [typeof(DamageForEnemyUpgrade)] = new()
        {
            [LootType.Wood] = 15,
        },

        [typeof(DamageForWoodUpgrade)] = new()
        {
            [LootType.Wood] = 15,
        },

        [typeof(DamageForOreUpgrade)] = new()
        {
            [LootType.Diamond] = 4,
            [LootType.Wood] = 15,
        },
    };

    public Dictionary<LootType, int> Get(Type upgradeType) => _baseUpgradesPrice[upgradeType].ToDictionary(x => x.Key, x => x.Value);
}