using Buffs;
using Buffs.AttackSpeed;
using Language;

namespace Skills.Skills
{
    public class AttackSpeedSkill : SkillBehaviour
    {
        private readonly AttackSpeedBuff _attackSpeedBuff = new ();
        private readonly CharacterBuffsModel _characterBuffsModel;
        private readonly float _attackCooldownMultiplierPerLevel = 0.1f;

        public AttackSpeedSkill(CharacterBuffsModel characterBuffsModel, LanguageProvider languageProvider)
            : base(languageProvider)
        {
            _characterBuffsModel = characterBuffsModel;
        }

        public override UpgradeType UpgradeType => UpgradeType.AttackCooldownReduce;
        public override SkillTickType SkillTickType => SkillTickType.AwakeTick;
        public override bool HasCooldown => false;
        private float AttackCooldownMultiplier => _attackCooldownMultiplierPerLevel * CurrentLevel;

        public override void Apply()
        {
            _characterBuffsModel.Add(_attackSpeedBuff);
            _attackSpeedBuff.SetParameters(AttackCooldownMultiplier);
        }

        public override void Disable()
        {
            _characterBuffsModel.Remove(_attackSpeedBuff);
        }

        public override string GetUpLevelDescription()
        {
            return $"{LanguageProvider.AttackCooldown}\n{AttackCooldownMultiplier} + {Decorate(_attackCooldownMultiplierPerLevel.ToString())}";
        }

        protected override void OnLevelChange()
        {
            _attackSpeedBuff.SetParameters(AttackCooldownMultiplier);
        }
    }
}