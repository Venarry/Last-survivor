public class ExperienceMultiplierUpgrade : ParametersUpgradeBehaviour
{
    private readonly ExperienceMultiplierBuff _buff = new();
    private readonly float _multiplierByLevel = 0.1f;

    public ExperienceMultiplierUpgrade(
        CharacterBuffsModel characterBuffsModel, ILanguageProvider languageProvider) : base(characterBuffsModel, languageProvider)
    {
    }

    private float ExperienceMultiplier => 1 + CurrentLevel * _multiplierByLevel;

    public override UpgradeType UpgradeType => UpgradeType.ExperienceMultiplier;

    public override void Apply()
    {
        CharacterBuffsModel.Add(_buff);
        _buff.SetParameters(ExperienceMultiplier);
    }

    public override void Disable()
    {
        CharacterBuffsModel.Remove(_buff);
    }

    protected override void OnLevelChange()
    {
        _buff.SetParameters(ExperienceMultiplier);
    }

    public override string GetUpLevelDescription()
    {
        return $"{LanguageProvider.ExperienceIncreaseHeader}\n{ExperienceMultiplier} + {Decorate(_multiplierByLevel.ToString())}";
    }
}