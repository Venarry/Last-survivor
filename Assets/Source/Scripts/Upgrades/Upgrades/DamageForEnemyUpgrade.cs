public class DamageForEnemyUpgrade : DamageUpgrade
{
    private readonly DamageForEnemyBuff _buff = new();

    public DamageForEnemyUpgrade(
        CharacterBuffsModel characterBuffsModel, ILanguageProvider languageProvider) : base(characterBuffsModel, languageProvider)
    {
    }

    public override UpgradeType UpgradeType => UpgradeType.DamageForEnemy;
    protected override DamageBuff DamageBuff => _buff;
    protected override string TargetName => LanguageProvider.TargetNameEnemy;
}