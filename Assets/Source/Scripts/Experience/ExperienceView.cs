using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceView : MonoBehaviour
{
    [SerializeField] private Image _levelBar;
    [SerializeField] private TMP_Text _levelLabel;

    private ExperienceModel _experienceModel;

    public void Init(ExperienceModel experienceModel)
    {
        _experienceModel = experienceModel;
        _experienceModel.ExperienceAdd += OnExperienceAdd;
        _experienceModel.LevelAdd += OnLevelAdd;

        OnExperienceAdd();
        OnLevelAdd();
    }

    private void OnDestroy()
    {
        _experienceModel.ExperienceAdd -= OnExperienceAdd;
        _experienceModel.LevelAdd -= OnLevelAdd;
    }

    public void Add(int experience)
    {
        _experienceModel.Add(experience);
    }

    private void OnExperienceAdd()
    {
        _levelBar.fillAmount = (float)_experienceModel.CurrentExperience / _experienceModel.ExperienceForNextLevel;
    }

    private void OnLevelAdd()
    {
        _levelLabel.text = $"LVL {_experienceModel.CurrentLevel}";
    }
}
