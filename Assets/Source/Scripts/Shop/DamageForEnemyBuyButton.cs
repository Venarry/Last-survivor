public class DamageForEnemyBuyButton : BuyDamageButton
{
    /*private DamageForEnemyUpgrade _damageForEnemyUpgrade;
    protected override Type GetUpgradeType() => typeof(DamageForEnemyUpgrade);

    protected override void OnInit()
    {
        _damageForEnemyUpgrade = UpgradesFactory.CreateDamageForEnemy();
        CharacterUpgrades.AddWithoutIncreaseLevel(_damageForEnemyUpgrade);
    }

    protected override void OnUpgradeBuy()
    {
        _damageForEnemyUpgrade.TryIncreaseLevel();
    }*/

    protected override DamageUpgrade CreateUpgrade() => 
        UpgradesFactory.CreateDamageForEnemy();
}
