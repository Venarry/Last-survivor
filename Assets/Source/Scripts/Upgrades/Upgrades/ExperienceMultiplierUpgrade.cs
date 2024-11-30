using Buffs;
using Buffs.Experience;
using Language;
using Skills;

namespace Upgrades.Upgrades
{
    public class ExperienceMultiplierUpgrade : ParametersUpgradeBehaviour
    {
        private readonly ExperienceMultiplierBuff _buff = new ();
        private readonly float _multiplierByLevel = 0.1f;

        public ExperienceMultiplierUpgrade(
            CharacterBuffsModel characterBuffsModel, LanguageProvider languageProvider)
            : base(characterBuffsModel, languageProvider)
        {
        }

        public override UpgradeType UpgradeType => UpgradeType.ExperienceMultiplier;
        private float ExperienceMultiplier => 1 + (CurrentLevel * _multiplierByLevel);

        public override void Apply()
        {
            CharacterBuffsModel.Add(_buff);
            _buff.SetParameters(ExperienceMultiplier);
        }

        public override void Disable()
        {
            CharacterBuffsModel.Remove(_buff);
        }

        public override string GetUpLevelDescription()
        {
            return $"{LanguageProvider.ExperienceIncreaseHeader}\n{ExperienceMultiplier} + {Decorate(_multiplierByLevel.ToString())}";
        }

        protected override void OnLevelChange()
        {
            _buff.SetParameters(ExperienceMultiplier);
        }
    }
}