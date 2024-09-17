public class DamageForOreBuyButton : BuyUpgradeButton
{
    public override UpgradeType UpgradeType => UpgradeType.DamageForOre;

    protected override ParametersUpgradeBehaviour CreateUpgrade() =>
        UpgradesFactory.CreateDamageForOre();
}