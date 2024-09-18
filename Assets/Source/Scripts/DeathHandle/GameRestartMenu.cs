using UnityEngine;
using UnityEngine.UI;

public class GameRestartMenu : MonoBehaviour
{
    [SerializeField] private GameObject _parent;
    [SerializeField] private Button _restartButton;
    [SerializeField] private DayCycle _dayCycle;
    [SerializeField] private MapGenerator _mapGenerator;

    private CharacterUpgradesModel<SkillBehaviour> _characterSkills;
    private ExperienceModel _characterExperience;
    private ThirdPersonMovement _thirdPersonMovement;
    private LevelsStatisticModel _levelsStatisticModel;
    private HealthModel _healthModel;
    private IProgressSaveService _progressSaveService;

    public void Init(
        CharacterUpgradesModel<SkillBehaviour> characterSkills,
        ExperienceModel characterExperience,
        ThirdPersonMovement thirdPersonMovement,
        LevelsStatisticModel levelsStatisticModel,
        HealthModel healthModel,
        IProgressSaveService progressSaveService)
    {
        _characterSkills = characterSkills;
        _characterExperience = characterExperience;
        _thirdPersonMovement = thirdPersonMovement;
        _levelsStatisticModel = levelsStatisticModel;
        _healthModel = healthModel;
        _progressSaveService = progressSaveService;
    }

    private void OnEnable()
    {
        _restartButton.onClick.AddListener(ResetLevel);
    }

    private void OnDisable()
    {
        _restartButton.onClick.RemoveListener(ResetLevel);
    }

    public void Show()
    {
        _parent.SetActive(true);
    }

    public void Hide()
    {
        _parent.SetActive(false);
    }

    private void ResetLevel()
    {
        _dayCycle.ResetTime();
        _mapGenerator.ResetLevels();
        _levelsStatisticModel.ResetToCheckpoint();
        _characterExperience.Reset();
        _characterSkills.RemoveAll();
        _healthModel.Restore();

        _thirdPersonMovement.gameObject.SetActive(true);
        _thirdPersonMovement.SetPosition(position: new(0, 0, 5));
        _thirdPersonMovement.SetBehaviour(state: true);

        Hide();
        _progressSaveService.Save();
    }
}
