public class DamageForWoodBuyButton : BuyUpgradeButton
{
    public override UpgradeType UpgradeType => UpgradeType.DamageForOre;

    protected override ParametersUpgradeBehaviour CreateUpgrade() =>
        UpgradesFactory.CreateDamageForWood();
}
