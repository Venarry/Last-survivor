public class DamageForOreBuyButton : BuyUpgradeButton
{
    protected override ParametersUpgradeBehaviour CreateUpgrade() =>
        UpgradesFactory.CreateDamageForOre();
}