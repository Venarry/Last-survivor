using UnityEngine;
using UnityEngine.UI;

public class ExperienceView : MonoBehaviour
{
    [SerializeField] private Image _levelBar;
    //[SerializeField] private TMP_Pro _levelBar;

    private ExperienceModel _experienceModel;

    public void Init(ExperienceModel experienceModel)
    {
        _experienceModel = experienceModel;
        _experienceModel.ExperienceAdd += OnExperienceAdd;
        _experienceModel.LevelAdd += OnLevelAdd;

        OnExperienceAdd();
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
        Debug.Log("level add");
    }
}
