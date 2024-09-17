public class DamageForEnemyBuyButton : BuyUpgradeButton
{
    protected override ParametersUpgradeBehaviour CreateUpgrade() => 
        UpgradesFactory.CreateDamageForEnemy();
}
