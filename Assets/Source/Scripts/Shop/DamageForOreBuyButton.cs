public class DamageForOreBuyButton : BuyUpgradeButton
{
    protected override void OnUpgradeBuy()
    {
        CharacterUpgrades.AddOrIncreaseLevel(UpgradesFactory.CreateDamageForOre());
    }
}