public class DamageForWoodBuyButton : BuyDamageButton
{
    protected override DamageUpgrade CreateUpgrade() =>
        UpgradesFactory.CreateDamageForWood();
}
