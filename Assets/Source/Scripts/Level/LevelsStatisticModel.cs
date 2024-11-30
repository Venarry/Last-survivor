using System;
using Configs;

namespace Level
{
    public class LevelsStatisticModel
    {
        public event Action Changed;

        public int TotalLevel { get; private set; }
        public int CurrentLevel => TotalLevel % GameParameters.LevelsForCheckpoint;
        public int NextWave => (CurrentLevel + 1) % GameParameters.LevelsForCheckpoint;

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
}