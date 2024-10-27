public class DamageForWoodUpgrade : DamageUpgrade
{
    private readonly DamageForWoodBuff _buff = new();

    public DamageForWoodUpgrade(
        CharacterBuffsModel characterBuffsModel, ILanguageProvider languageProvider) : base(characterBuffsModel, languageProvider)
    {
    }

    public override UpgradeType UpgradeType => UpgradeType.DamageForWood;
    protected override DamageBuff DamageBuff => _buff;
    protected override string TargetName => LanguageProvider.TargetNameWood;
}