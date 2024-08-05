public class LevelsStatistic
{
    public const int LevelForCheckpoint = 10;
    public int TotalWave { get; private set; } = 25;
    public int CurrentWave => TotalWave % 10;
}
