using System;

public class ExperienceModel
{
    private const int BaseExperienceForNextLevel = 10;

    public int CurrentExperience { get; private set; }
    public int CurrentLevel { get; private set; }
    public int ExperienceForNextLevel => (CurrentLevel + 1) * BaseExperienceForNextLevel;

    public event Action ExperienceAdd;
    public event Action LevelAdd;

    public void Add(int experience)
    {
        CurrentExperience += experience;
        TryUpLevel();

        ExperienceAdd.Invoke();
    }

    private void TryUpLevel()
    {
        while (CurrentExperience >= ExperienceForNextLevel)
        {
            //int experienceRemains = CurrentExperience - ExperienceForNextLevel;
            CurrentExperience -= ExperienceForNextLevel;
            CurrentLevel++;
            LevelAdd?.Invoke();
        }
    }
}
