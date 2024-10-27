using System;
using System.Collections.Generic;

public class ParameterUpgradesFactory
{
    private readonly CharacterBuffsModel _characterBuffsModel;
    private readonly ILanguageProvider _languageProvider;
    private readonly Dictionary<UpgradeType, Func<ParametersUpgradeBehaviour>> _upgradesByType;

    public ParameterUpgradesFactory(CharacterBuffsModel characterBuffsModel, ILanguageProvider languageProvider)
    {
        _characterBuffsModel = characterBuffsModel;
        _languageProvider = languageProvider;

        _upgradesByType = new()
        {
            [UpgradeType.DamageForEnemy] = CreateDamageForEnemy,
            [UpgradeType.DamageForWood] = CreateDamageForWood,
            [UpgradeType.DamageForOre] = CreateDamageForOre,
            [UpgradeType.DayIncrease] = CreateDayIncrease,
            [UpgradeType.ExperienceMultiplier] = CreateExperienceMultiplier,
        };
    }

    public DamageForEnemyUpgrade CreateDamageForEnemy() => new(_characterBuffsModel, _languageProvider);
    public DamageForWoodUpgrade CreateDamageForWood() => new(_characterBuffsModel, _languageProvider);
    public DamageForOreUpgrade CreateDamageForOre() => new(_characterBuffsModel, _languageProvider);
    public DayIncreaseUpgrade CreateDayIncrease() => new(_characterBuffsModel, _languageProvider);
    public ExperienceMultiplierUpgrade CreateExperienceMultiplier() => new(_characterBuffsModel, _languageProvider);

    public ParametersUpgradeBehaviour CreateBy(UpgradeType upgradeType, int level)
    {
        ParametersUpgradeBehaviour upgrade = _upgradesByType[upgradeType]();
        upgrade.SetLevel(level);

        return upgrade;
    }
}
