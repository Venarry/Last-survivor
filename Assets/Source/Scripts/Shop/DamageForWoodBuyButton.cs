public class DamageForWoodBuyButton : BuyUpgradeButton
{
    protected override void OnUpgradeBuy()
    {
        CharacterUpgrades.Add(UpgradesFactory.CreateDamageForWood());
    }
}
