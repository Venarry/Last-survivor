public class DamageForWoodBuyButton : BuyUpgradeButton
{
    protected override ParametersUpgradeBehaviour CreateUpgrade() =>
        UpgradesFactory.CreateDamageForWood();
}
