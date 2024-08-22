public class DamageForOreUpgrade : DamageUpgrade
{
    private readonly DamageForOreBuff _buff = new();

    public DamageForOreUpgrade(CharacterBuffsModel characterBuffsModel) : base(characterBuffsModel)
    {
    }

    protected override DamageBuff DamageBuff => _buff;
}