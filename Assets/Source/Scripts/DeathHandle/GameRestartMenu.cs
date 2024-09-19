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
    private TargetsProvider<Loot> _lootProvider;
    private IProgressSaveService _progressSaveService;
    private Vector3 _spawnPosition;

    public void Init(
        CharacterUpgradesModel<SkillBehaviour> characterSkills,
        ExperienceModel characterExperience,
        ThirdPersonMovement thirdPersonMovement,
        LevelsStatisticModel levelsStatisticModel,
        HealthModel healthModel,
        TargetsProvider<Loot> lootProvider,
        IProgressSaveService progressSaveService,
        Vector3 spawnPosition)
    {
        _characterSkills = characterSkills;
        _characterExperience = characterExperience;
        _thirdPersonMovement = thirdPersonMovement;
        _levelsStatisticModel = levelsStatisticModel;
        _healthModel = healthModel;
        _lootProvider = lootProvider;
        _progressSaveService = progressSaveService;
        _spawnPosition = spawnPosition;
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

        foreach (Loot target in _lootProvider.GetAll())
        {
            target.PlaceInPool();
        } 

        _thirdPersonMovement.gameObject.SetActive(true);
        _thirdPersonMovement.SetPosition(_spawnPosition);
        _thirdPersonMovement.SetBehaviour(state: true);

        Hide();
        _progressSaveService.Save();
    }
}
