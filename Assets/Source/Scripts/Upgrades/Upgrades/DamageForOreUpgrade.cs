public class DamageForOreUpgrade : DamageUpgrade
{
    private readonly DamageForOreBuff _buff = new();

    public DamageForOreUpgrade(CharacterBuffsModel characterBuffsModel) : base(characterBuffsModel)
    {
    }

    public override UpgradeType UpgradeType => UpgradeType.DamageForOre;
    protected override DamageBuff DamageBuff => _buff;
}