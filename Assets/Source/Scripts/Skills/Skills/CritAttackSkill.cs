using Buffs;
using Buffs.CritDamage;
using Language;
using UnityEngine;

namespace Skills.Skills
{
    public class CritAttackSkill : SkillBehaviour
    {
        private readonly CharacterBuffsModel _characterBuffsModel;
        private readonly CritDamageBuff _critDamageBuff = new ();

        private readonly float _critDamageMultiplierPerLevel = 0.2f;
        private readonly float _critChancePerLevel = 10;

        private readonly float _baseCritDamageMultiplier = 1.3f;
        private readonly float _baseCritChance = 20;

        public CritAttackSkill(CharacterBuffsModel characterBuffsModel, LanguageProvider languageProvider)
            : base(languageProvider)
        {
            _characterBuffsModel = characterBuffsModel;
        }

        public override UpgradeType UpgradeType => UpgradeType.CritAttack;
        public override SkillTickType SkillTickType => SkillTickType.AwakeTick;
        public override bool HasCooldown => false;
        private float CritDamage => _baseCritDamageMultiplier + (_critDamageMultiplierPerLevel * Mathf.Max(CurrentLevel - 1, 0));
        private float CritChance => _baseCritChance + (_critChancePerLevel * Mathf.Max(CurrentLevel - 1, 0));

        public override void Apply()
        {
            _characterBuffsModel.Add(_critDamageBuff);
            _critDamageBuff.SetParameters(CritDamage, CritChance);
        }

        public override void Disable()
        {
            _characterBuffsModel.Remove(_critDamageBuff);
        }

        public override string GetUpLevelDescription()
        {
            string critDamageUpgradeText = string.Empty;
            string critChanceUpgradeText = string.Empty;

            if (CurrentLevel > 0)
            {
                critDamageUpgradeText = $"(+{Decorate((_critDamageMultiplierPerLevel * 100).ToString())})";
                critChanceUpgradeText = $"(+{Decorate(_critChancePerLevel.ToString())})";
            }

            return $"{LanguageProvider.CritDamage} {CritDamage * 100}% {critDamageUpgradeText}\n" +
                $"{LanguageProvider.CritChance} {CritChance}% {critChanceUpgradeText}";
        }

        protected override void OnLevelChange()
        {
            _critDamageBuff.SetParameters(CritDamage, CritChance);
        }
    }
}