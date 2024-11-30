using Skills;
using Upgrades;

namespace Shop
{
    public class DamageForWoodBuyButton : BuyUpgradeButton
    {
        public override UpgradeType UpgradeType => UpgradeType.DamageForWood;

        protected override ParametersUpgradeBehaviour CreateUpgrade() =>
            UpgradesFactory.CreateDamageForWood();
    }
}