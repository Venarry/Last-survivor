public class DamageForEnemyBuyButton : BuyUpgradeButton
{
    public override UpgradeType UpgradeType => UpgradeType.DamageForEnemy;

    protected override ParametersUpgradeBehaviour CreateUpgrade() => 
        UpgradesFactory.CreateDamageForEnemy();
}
