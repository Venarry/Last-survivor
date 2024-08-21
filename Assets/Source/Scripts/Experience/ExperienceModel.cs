using System;
using UnityEngine;

public class ExperienceModel
{
    private bool _disabled = false;

    public float CurrentExperience { get; private set; }
    public int CurrentLevel { get; private set; }
    public float ExperienceForNextLevel => Mathf.Pow(CurrentLevel + 1, 2) * GameParamenters.BaseExperienceForNextLevel;
    //public float ExperienceForNextLevel => (CurrentLevel + 1) * GameParamenters.BaseExperienceForNextLevel;

    public event Action ExperienceChanged;
    public event Action LevelAdded;
    public event Action LevelsRemoved;

    public void Add(float experience)
    {
        if (_disabled == true)
            return;

        CurrentExperience += experience;
        TryUpLevel();

        ExperienceChanged.Invoke();
    }

    public void EnableBeahviour()
    {
        _disabled = false;
    }

    public void DisableBehaviour()
    {
        _disabled = true;
    }

    public void Reset()
    {
        CurrentExperience = 0;
        CurrentLevel = 0;
        ExperienceChanged?.Invoke();
        LevelsRemoved?.Invoke();
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
