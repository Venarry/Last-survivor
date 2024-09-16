public class DamageForOreBuyButton : BuyDamageButton
{
    protected override DamageUpgrade CreateUpgrade() =>
        UpgradesFactory.CreateDamageForOre();
}