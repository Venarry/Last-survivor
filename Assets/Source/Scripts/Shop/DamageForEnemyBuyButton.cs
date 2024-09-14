public class DamageForEnemyBuyButton : BuyUpgradeButton
{
    protected override void OnUpgradeBuy()
    {
        CharacterUpgrades.AddOrIncreaseLevel(UpgradesFactory.CreateDamageForEnemy());
    }
}
