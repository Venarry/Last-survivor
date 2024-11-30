using Buffs;
using Language;
using Skills;

namespace Upgrades
{
    public abstract class ParametersUpgradeBehaviour : Upgrade
    {
        protected ParametersUpgradeBehaviour(
            CharacterBuffsModel characterBuffsModel, LanguageProvider languageProvider)
            : base(languageProvider)
        {
            CharacterBuffsModel = characterBuffsModel;
        }

        public override int MaxLevel { get; } = int.MaxValue;
        public override SkillTickType SkillTickType => SkillTickType.AwakeTick;
        public override bool HasCooldown => false;
        protected CharacterBuffsModel CharacterBuffsModel { get; private set; }
    }
}