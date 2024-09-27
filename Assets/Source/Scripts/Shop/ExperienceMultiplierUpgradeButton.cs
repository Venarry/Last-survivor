public class ExperienceMultiplierUpgradeButton : BuyUpgradeButton
{
    public override UpgradeType UpgradeType => UpgradeType.ExperienceMultiplier;

    protected override ParametersUpgradeBehaviour CreateUpgrade() =>
        UpgradesFactory.CreateExperienceMultiplier();
}