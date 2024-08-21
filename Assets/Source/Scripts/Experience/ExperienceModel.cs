using System;

public class ExperienceModel
{
    public int CurrentExperience { get; private set; }
    public int CurrentLevel { get; private set; }
    public int ExperienceForNextLevel => (CurrentLevel + 1) * GameParamenters.BaseExperienceForNextLevel;

    public event Action ExperienceChanged;
    public event Action LevelAdded;
    public event Action LevelRemoved;

    public void Add(int experience)
    {
        CurrentExperience += experience;
        TryUpLevel();

        ExperienceChanged.Invoke();
    }

    public void Reset()
    {
        CurrentExperience = 0;
        CurrentLevel = 0;
        ExperienceChanged?.Invoke();
        LevelRemoved?.Invoke();
    }

    private void TryUpLevel()
    {
        while (CurrentExperience >= ExperienceForNextLevel)
        {
            CurrentExperience -= ExperienceForNextLevel;
            CurrentLevel++;
            LevelAdded?.Invoke();
        }
    }
}
