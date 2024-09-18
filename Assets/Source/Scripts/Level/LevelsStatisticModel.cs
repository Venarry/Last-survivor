using System;

public class LevelsStatisticModel
{
    public int TotalLevel { get; private set; }
    public int CurrentLevel => TotalLevel % GameParamenters.LevelsForCheckpoint;
    public int NextWave => (CurrentLevel + 1) % GameParamenters.LevelsForCheckpoint;

    public event Action Changed;

    public void Add()
    {
        TotalLevel++;
        Changed?.Invoke();
    }

    public void Set(int count )
    {
        TotalLevel = count;
    }

    public void ResetToCheckpoint()
    {
        TotalLevel -= CurrentLevel;
        Changed?.Invoke();
    }
}
