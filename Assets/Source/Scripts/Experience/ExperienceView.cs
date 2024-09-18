using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceView : MonoBehaviour
{
    [SerializeField] private Image _levelBar;
    [SerializeField] private TMP_Text _levelLabel;
    [SerializeField] private ParticleSystem _levelUpParticlePrefab;

    private ExperienceModel _experienceModel;

    public void Init(ExperienceModel experienceModel)
    {
        _experienceModel = experienceModel;

        _experienceModel.ExperienceChanged += OnExperienceChange;
        _experienceModel.LevelAdded += OnLevelAdd;
        _experienceModel.LevelsRemoved += OnLevelRemove;
        _experienceModel.DataLoaded += RefreshAllInforamtion;

        RefreshAllInforamtion();
    }

    private void OnDestroy()
    {
        _experienceModel.ExperienceChanged -= OnExperienceChange;
        _experienceModel.LevelAdded -= OnLevelAdd;
        _experienceModel.LevelsRemoved -= OnLevelRemove;
        _experienceModel.DataLoaded -= RefreshAllInforamtion;
    }

    public void Add(float experience)
    {
        _experienceModel.Add(experience);
    }

    private void OnExperienceChange()
    {
        _levelBar.fillAmount = (float)_experienceModel.CurrentExperience / _experienceModel.ExperienceForNextLevel;
    }

    private void OnLevelAdd()
    {
        RefreshLevelLabel();

        if(_levelUpParticlePrefab != null)
            Instantiate(_levelUpParticlePrefab, transform.position, Quaternion.identity);
    }

    private void RefreshAllInforamtion()
    {
        OnExperienceChange();
        RefreshLevelLabel();
    }

    private void OnLevelRemove()
    {
        RefreshLevelLabel();
    }

    private void RefreshLevelLabel()
    {
        _levelLabel.text = $"LVL {_experienceModel.CurrentLevel}";
    }
}
