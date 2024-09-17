using System.Collections.Generic;
using System.Linq;

public class PricesDataSource
{
    private readonly Dictionary<UpgradeType, Dictionary<LootType, int>> _baseUpgradesPrice = new()
    {
        [UpgradeType.DamageForEnemy] = new()
        {
            [LootType.Wood] = 15,
        },

        [UpgradeType.DamageForWood] = new()
        {
            [LootType.Wood] = 15,
        },

        [UpgradeType.DamageForOre] = new()
        {
            [LootType.Diamond] = 4,
            [LootType.Wood] = 15,
        },

        [UpgradeType.DayIncrease] = new()
        {
            [LootType.Prestige] = 1,
        },
    };

    public Dictionary<LootType, int> Get(UpgradeType upgradeType) => _baseUpgradesPrice[upgradeType].ToDictionary(x => x.Key, x => x.Value);
}