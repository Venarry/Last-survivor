using Buffs;
using Buffs.DayIncrease;
using Language;
using Skills;

namespace Upgrades.Upgrades
{
    public class DayIncreaseUpgrade : ParametersUpgradeBehaviour
    {
        private readonly DayIncreaseBuff _dayIncreaseBuff = new ();
        private readonly float _durationByLevel = 0.2f;

        public DayIncreaseUpgrade(
            CharacterBuffsModel characterBuffsModel, LanguageProvider languageProvider)
            : base(characterBuffsModel, languageProvider)
        {
        }

        public override UpgradeType UpgradeType => UpgradeType.DayIncrease;

        public override void Apply()
        {
            CharacterBuffsModel.Add(_dayIncreaseBuff);
            _dayIncreaseBuff.SetParameters(_durationByLevel * CurrentLevel);
        }

        public override void Disable()
        {
            CharacterBuffsModel.Remove(_dayIncreaseBuff);
        }

        public override string GetUpLevelDescription()
        {
            return $"{LanguageProvider.DayDurationHeader}:\n{_durationByLevel * CurrentLevel} + {Decorate(_durationByLevel.ToString())}";
        }

        protected override void OnLevelChange()
        {
            _dayIncreaseBuff.SetParameters(_durationByLevel * CurrentLevel);
        }
    }
}