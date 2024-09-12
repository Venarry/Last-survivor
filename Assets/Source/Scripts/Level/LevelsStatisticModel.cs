using System;

public class LevelsStatisticModel
{
    public int TotalLevel { get; private set; }
    public int CurrentLevel => TotalLevel % GameParamenters.LevelsForCheckpoint;
    public int NextWave => (CurrentLevel + 1) % GameParamenters.LevelsForCheckpoint;

    public event Action Added;

    public void Add()
    {
        TotalLevel++;
        Added?.Invoke();
    }

    public void Set(int count )
    {
        TotalLevel = count;
    }
}
