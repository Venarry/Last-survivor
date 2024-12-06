using Buffs;
using Buffs.Damage;
using Language;
using Skills;
using Targets;

namespace Upgrades.Upgrades
{
    public class DamageUpgrade : ParametersUpgradeBehaviour
    {
        private readonly UpgradeType _upgradeType;

        public DamageUpgrade(
            TargetType targetType,
            UpgradeType upgradeType,
            CharacterBuffsModel characterBuffsModel,
            LanguageProvider languageProvider)
            : base(characterBuffsModel, languageProvider)
        {
            _upgradeType = upgradeType;
            _damageBuff = new (targetType);
            _targetName = languageProvider.GetTargetName(targetType);
        }

        public override UpgradeType UpgradeType => _upgradeType;
        private readonly DamageBuff _damageBuff;
        private readonly string _targetName;

        protected virtual float DamagePerLevel { get; } = 0.1f;
        private float Damage => DamagePerLevel * CurrentLevel;

        public override void Apply()
        {
            CharacterBuffsModel.Add(_damageBuff);
            _damageBuff.SetParameters(Damage);
        }

        public override void Disable()
        {
            CharacterBuffsModel.Remove(_damageBuff);
        }

        public override string GetUpLevelDescription()
        {
            string description = $"{LanguageProvider.AdditionalDamageHeader} {_targetName}:\n{CurrentLevel * DamagePerLevel} + {Decorate(DamagePerLevel.ToString())}";

            return description;
        }

        protected override void OnLevelChange()
        {
            _damageBuff.SetParameters(Damage);
        }
    }
}