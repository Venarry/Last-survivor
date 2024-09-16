using System;

public abstract class BuyDamageButton : BuyUpgradeButton
{
    protected DamageUpgrade DamageUpgrade { get; set; }
    protected override Type GetUpgradeType() => DamageUpgrade.GetType();

    protected override void OnInit()
    {
        DamageUpgrade = CreateUpgrade();
        CharacterUpgrades.AddWithoutIncreaseLevel(DamageUpgrade);
    }

    protected override void OnUpgradeBuy()
    {
        DamageUpgrade.TryIncreaseLevel();
    }

    protected abstract DamageUpgrade CreateUpgrade();
}
