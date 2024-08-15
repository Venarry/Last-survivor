public class DamageForEnemyBuyButton : BuyUpgradeButton
{
    protected override void OnUpgradeBuy()
    {
        CharacterUpgrades.Add(UpgradesFactory.CreateDamageForEnemy());
    }
}
