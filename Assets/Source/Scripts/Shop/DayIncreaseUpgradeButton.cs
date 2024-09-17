public class DayIncreaseUpgradeButton : BuyUpgradeButton
{
    public override UpgradeType UpgradeType => UpgradeType.DayIncrease;

    protected override ParametersUpgradeBehaviour CreateUpgrade() =>
        UpgradesFactory.CreateDayIncrease();
}