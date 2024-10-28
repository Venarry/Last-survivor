public class DamageForOreUpgrade : DamageUpgrade
{
    private readonly DamageForOreBuff _buff = new();

    public DamageForOreUpgrade(
        CharacterBuffsModel characterBuffsModel, LanguageProvider languageProvider) : base(characterBuffsModel, languageProvider)
    {
    }

    public override UpgradeType UpgradeType => UpgradeType.DamageForOre;
    protected override DamageBuff DamageBuff => _buff;
    protected override string TargetName => LanguageProvider.TargetNameOre;
}
