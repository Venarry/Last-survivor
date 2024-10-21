using System;

public class LevelsStatisticModel
{
    public int TotalLevel { get; private set; }
    public int CurrentLevel => TotalLevel % GameParameters.LevelsForCheckpoint;
    public int NextWave => (CurrentLevel + 1) % GameParameters.LevelsForCheckpoint;

    public event Action Changed;

    public void Add()
    {
        TotalLevel++;
        Changed?.Invoke();
    }

    public void Set(int count)
    {
        TotalLevel = count;
        Changed?.Invoke();
    }

    public void ResetToCheckpoint()
    {
        TotalLevel -= CurrentLevel;
        Changed?.Invoke();
    }
}
