using System;
using UnityEngine;

public class ExperienceModel
{
    private readonly CharacterBuffsModel _characterBuffsModel;

    public ExperienceModel(CharacterBuffsModel characterBuffsModel)
    {
        _characterBuffsModel = characterBuffsModel;
    }

    private bool _disabled = false;

    public float CurrentExperience { get; private set; }
    public int CurrentLevel { get; private set; }
    public float ExperienceForNextLevel => Mathf.Pow(CurrentLevel + 1, 2) * GameParameters.BaseExperienceForNextLevel;

    public event Action ExperienceChanged;
    public event Action LevelAdded;
    public event Action LevelsRemoved;
    public event Action DataLoaded;

    public void Add(float experience)
    {
        if (_disabled == true)
            return;

        float buffedExperience = experience;

        foreach (IExperienceBuff buff in _characterBuffsModel.GetBuffs<IExperienceBuff>())
        {
            buffedExperience = buff.Apply(buffedExperience);
        }

        CurrentExperience += buffedExperience;
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

    public void Load(int level, float experience)
    {
        CurrentLevel = level;
        CurrentExperience = experience;

        DataLoaded?.Invoke();
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
