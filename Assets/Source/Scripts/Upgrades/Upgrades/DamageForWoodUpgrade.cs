public class DamageForWoodUpgrade : DamageUpgrade
{
    private readonly DamageForWoodBuff _buff = new();

    public DamageForWoodUpgrade(CharacterBuffsModel characterBuffsModel) : base(characterBuffsModel)
    {
    }

    public override UpgradeType UpgradeType => UpgradeType.DamageForWood;
    protected override DamageBuff DamageBuff => _buff;
}