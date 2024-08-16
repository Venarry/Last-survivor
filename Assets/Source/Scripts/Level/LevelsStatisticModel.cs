using UnityEngine;

public class LevelsStatisticModel
{
    public int TotalWave { get; private set; }
    public int CurrentWave => TotalWave % GameParamenters.LevelForCheckpoint;
    public int NextWave => (CurrentWave + 1) % GameParamenters.LevelForCheckpoint;

    public void Add()
    {
        TotalWave++;
    }
}
