using YG;

public class LeaderboardSaver
{
    private const string LeaderboardName = "Max level";

    private readonly LevelsStatisticModel _levelsStatisticModel;
    private readonly IMaxLevelProvider _maxLevelProvider;

    public LeaderboardSaver(
        LevelsStatisticModel levelsStatisticModel,
        IMaxLevelProvider maxLevelProvider)
    {
        _levelsStatisticModel = levelsStatisticModel;
        _maxLevelProvider = maxLevelProvider;
    }

    public void Enable()
    {
        _levelsStatisticModel.Changed += OnLevelChange;
    }

    public void Disable()
    {
        _levelsStatisticModel.Changed -= OnLevelChange;
    }

    private void OnLevelChange()
    {
        if(_levelsStatisticModel.TotalLevel >= _maxLevelProvider.MaxLevel)
        {
            YandexGame.NewLeaderboardScores(LeaderboardName, _levelsStatisticModel.TotalLevel);
        }
    }
}