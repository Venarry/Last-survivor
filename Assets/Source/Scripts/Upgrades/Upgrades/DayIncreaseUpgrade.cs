using System;
using YG;

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

    public override void Disable()
    {
        CharacterBuffsModel.Remove(_dayIncreaseBuff);
    }

    protected override void OnLevelChange()
    {
        _dayIncreaseBuff.SetParameters(_durationByLevel * CurrentLevel);
    }

    public override string GetUpLevelDescription()
    {
        string dayDurationHeader;

        switch (YandexGame.lang)
        {
            case GameParameters.CodeRu:
                dayDurationHeader = "Увеличение длительности дня";
                break;

            case GameParameters.CodeTr:
                dayDurationHeader = "Artan süre";
                break;

            default:
                dayDurationHeader = "Increase day duration";
                break;
        }

        return $"{dayDurationHeader}:\n{_durationByLevel * CurrentLevel} + {Decorate(_durationByLevel.ToString())}";
    }
}