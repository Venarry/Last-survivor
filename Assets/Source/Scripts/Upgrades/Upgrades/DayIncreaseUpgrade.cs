public class DayIncreaseUpgrade : ParametersUpgradeBehaviour
{
    private readonly DayIncreaseBuff _dayIncreaseBuff = new();
    private readonly float _durationByLevel = 0.2f;

    public DayIncreaseUpgrade(CharacterBuffsModel characterBuffsModel) : base(characterBuffsModel)
    {
    }

    public override UpgradeType UpgradeType => UpgradeType.DayIncrease;

    public override void Apply()
    {
        CharacterBuffsModel.Add(_dayIncreaseBuff);
        _dayIncreaseBuff.SetParameters(_durationByLevel * CurrentLevel);
    }

    protected override void OnLevelChange()
    {
        _dayIncreaseBuff.SetParameters(_durationByLevel * CurrentLevel);
    }

    public override string GetUpLevelDescription()
    {
        return $"Increase day duration:\n{_durationByLevel * CurrentLevel} + {Decorate(_durationByLevel.ToString())}";
    }
}