public class DamageForEnemyUpgrade : DamageUpgrade
{
    private readonly DamageForEnemyBuff _buff = new();

    public DamageForEnemyUpgrade(CharacterBuffsModel characterBuffsModel) : base(characterBuffsModel)
    {
    }

    protected override DamageBuff DamageBuff => _buff;
}