using System;
using System.Collections.Generic;

public class ParameterUpgradesFactory
{
    private readonly CharacterBuffsModel _characterBuffsModel;
    private readonly Dictionary<UpgradeType, Func<ParametersUpgradeBehaviour>> _upgradesByType;

    public ParameterUpgradesFactory(CharacterBuffsModel characterBuffsModel)
    {
        _characterBuffsModel = characterBuffsModel;

        _upgradesByType = new()
        {
            [UpgradeType.DamageForEnemy] = CreateDamageForEnemy,
            [UpgradeType.DamageForWood] = CreateDamageForWood,
            [UpgradeType.DamageForOre] = CreateDamageForOre,
        };
    }

    public DamageForEnemyUpgrade CreateDamageForEnemy() => new(_characterBuffsModel);
    public DamageForWoodUpgrade CreateDamageForWood() => new(_characterBuffsModel);
    public DamageForOreUpgrade CreateDamageForOre() => new(_characterBuffsModel);

    public ParametersUpgradeBehaviour CreateBy(UpgradeType upgradeType, int level)
    {
        ParametersUpgradeBehaviour upgrade = _upgradesByType[upgradeType]();
        upgrade.SetLevel(level);

        return upgrade;
    }
}
