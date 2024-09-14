public class DamageForWoodBuyButton : BuyUpgradeButton
{
    protected override void OnUpgradeBuy()
    {
        CharacterUpgrades.AddOrIncreaseLevel(UpgradesFactory.CreateDamageForWood());
    }
}
