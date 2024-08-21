using System;
using UnityEngine;

public class LevelsStatisticModel
{
    public int TotalWave { get; private set; }
    public int CurrentWave => TotalWave % GameParamenters.LevelsForCheckpoint;
    public int NextWave => (CurrentWave + 1) % GameParamenters.LevelsForCheckpoint;

    public event Action Added;

    public void Add()
    {
        TotalWave++;
        Added?.Invoke();
    }
}
