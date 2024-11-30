using Buffs;
using Buffs.Damage;
using Language;
using Skills;

namespace Upgrades.Upgrades
{
    public class DamageForEnemyUpgrade : DamageUpgrade
    {
        private readonly DamageForEnemyBuff _buff = new();

        public DamageForEnemyUpgrade(
            CharacterBuffsModel characterBuffsModel, LanguageProvider languageProvider)
            : base(characterBuffsModel, languageProvider)
        {
        }

        public override UpgradeType UpgradeType => UpgradeType.DamageForEnemy;
        protected override DamageBuff DamageBuff => _buff;
        protected override string TargetName => LanguageProvider.TargetNameEnemy;
    }
}