public class DamageForOreBuyButton : BuyUpgradeButton
{
    protected override void OnUpgradeBuy()
    {
        CharacterUpgrades.Add(UpgradesFactory.CreateDamageForOre());
    }
}