public class DamageForEnemyUpgrade : DamageUpgrade
{
    private readonly DamageForEnemyBuff _buff = new();

    public DamageForEnemyUpgrade(CharacterBuffsModel characterBuffsModel) : base(characterBuffsModel)
    {
    }

    public override UpgradeType UpgradeType => UpgradeType.DamageForEnemy;
    protected override DamageBuff DamageBuff => _buff;
}