using Buffs;
using Buffs.Damage;
using Language;
using Skills;

namespace Upgrades.Upgrades
{
    public class DamageForWoodUpgrade : DamageUpgrade
    {
        private readonly DamageForWoodBuff _buff = new ();

        public DamageForWoodUpgrade(
            CharacterBuffsModel characterBuffsModel, LanguageProvider languageProvider)
            : base(characterBuffsModel, languageProvider)
        {
        }

        public override UpgradeType UpgradeType => UpgradeType.DamageForWood;
        protected override DamageBuff DamageBuff => _buff;
        protected override string TargetName => LanguageProvider.TargetNameWood;
    }
}