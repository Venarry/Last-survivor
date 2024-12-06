using System;
using System.Collections.Generic;
using Buffs;
using Language;
using Skills;
using Targets;
using Upgrades.Upgrades;

namespace Upgrades
{
    public class ParameterUpgradesFactory
    {
        private readonly CharacterBuffsModel _characterBuffsModel;
        private readonly LanguageProvider _languageProvider;
        private readonly Dictionary<UpgradeType, Func<ParametersUpgradeBehaviour>> _upgradesByType;

        public ParameterUpgradesFactory(CharacterBuffsModel characterBuffsModel, LanguageProvider languageProvider)
        {
            _characterBuffsModel = characterBuffsModel;
            _languageProvider = languageProvider;

            _upgradesByType = new ()
            {
                [UpgradeType.DamageForEnemy] = CreateDamageForEnemy,
                [UpgradeType.DamageForWood] = CreateDamageForWood,
                [UpgradeType.DamageForOre] = CreateDamageForOre,
                [UpgradeType.DayIncrease] = CreateDayIncrease,
                [UpgradeType.ExperienceMultiplier] = CreateExperienceMultiplier,
            };
        }

        public DamageUpgrade CreateDamageForEnemy() => new (TargetType.Enemy, UpgradeType.DamageForEnemy, _characterBuffsModel, _languageProvider);
        public DamageUpgrade CreateDamageForWood() => new (TargetType.Wood, UpgradeType.DamageForWood, _characterBuffsModel, _languageProvider);
        public DamageUpgrade CreateDamageForOre() => new (TargetType.Ore, UpgradeType.DamageForOre, _characterBuffsModel, _languageProvider);
        public DayIncreaseUpgrade CreateDayIncrease() => new (_characterBuffsModel, _languageProvider);
        public ExperienceMultiplierUpgrade CreateExperienceMultiplier() => new (_characterBuffsModel, _languageProvider);

        public ParametersUpgradeBehaviour CreateBy(UpgradeType upgradeType, int level = 0)
        {
            ParametersUpgradeBehaviour upgrade = _upgradesByType[upgradeType]();
            upgrade.SetLevel(level);

            return upgrade;
        }
    }
}