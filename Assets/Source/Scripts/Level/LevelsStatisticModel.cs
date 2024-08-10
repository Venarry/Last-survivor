using UnityEngine;

public class LevelsStatisticModel
{
    public const int LevelForCheckpoint = 10;
    public int TotalWave { get; private set; }
    public int CurrentWave => TotalWave % LevelForCheckpoint;

    public void Add()
    {
        TotalWave++;
        Debug.Log(TotalWave);
    }
}
