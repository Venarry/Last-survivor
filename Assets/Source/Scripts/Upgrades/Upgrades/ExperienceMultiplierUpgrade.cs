public class ExperienceMultiplierUpgrade : ParametersUpgradeBehaviour
{
    private readonly ExperienceMultiplierBuff _buff = new();
    private readonly float _multiplierByLevel = 0.1f;

    public ExperienceMultiplierUpgrade(CharacterBuffsModel characterBuffsModel) : base(characterBuffsModel)
    {
    }

    private float ExperienceMultiplier => 1 + CurrentLevel * _multiplierByLevel;

    public override UpgradeType UpgradeType => UpgradeType.ExperienceMultiplier;

    public override void Apply()
    {
        CharacterBuffsModel.Add(_buff);
        _buff.SetParameters(ExperienceMultiplier);
    }

    protected override void OnLevelChange()
    {
        _buff.SetParameters(ExperienceMultiplier);
    }

    public override string GetUpLevelDescription()
    {
        return $"Increase experience multiplier\n{ExperienceMultiplier} + {Decorate(_multiplierByLevel.ToString())}";
    }
}