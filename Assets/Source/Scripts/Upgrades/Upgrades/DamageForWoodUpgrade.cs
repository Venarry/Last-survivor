public class DamageForWoodUpgrade : DamageUpgrade
{
    private readonly DamageForWoodBuff _buff = new();

    public DamageForWoodUpgrade(CharacterBuffsModel characterBuffsModel) : base(characterBuffsModel)
    {
    }

    protected override DamageBuff DamageBuff => _buff;
}