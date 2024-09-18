using UnityEngine;
using UnityEngine.UI;

public class DeathMenu : MonoBehaviour
{
    [SerializeField] private Button _restartButton;

    private CharacterUpgradesModel<SkillBehaviour> _characterSkills;
    private ExperienceModel _characterExperience;
    private ThirdPersonMovement _thirdPersonMovement;
    private MapGenerator _mapGenerator;
    private LevelsStatisticModel _levelsStatisticModel;
    private HealthModel _healthModel;

    public void Init(
        CharacterUpgradesModel<SkillBehaviour> characterSkills,
        ExperienceModel characterExperience,
        ThirdPersonMovement thirdPersonMovement,
        MapGenerator mapGenerator,
        LevelsStatisticModel levelsStatisticModel,
        HealthModel healthModel)
    {
        _characterSkills = characterSkills;
        _characterExperience = characterExperience;
        _thirdPersonMovement = thirdPersonMovement;
        _mapGenerator = mapGenerator;
        _levelsStatisticModel = levelsStatisticModel;
        _healthModel = healthModel;
    }

    private void OnEnable()
    {
        _restartButton.onClick.AddListener(ReseLevel);
    }

    private void OnDisable()
    {
        _restartButton.onClick.RemoveListener(ReseLevel);
    }

    private void ReseLevel()
    {
        _thirdPersonMovement.SetPosition(position: new(0, 0, 5));
        _mapGenerator.ResetLevels();
        _levelsStatisticModel.ResetToCheckpoint();
        _characterExperience.Reset();
        _characterSkills.RemoveAll();
        _healthModel.Restore();

        _thirdPersonMovement.SetBehaviour(state: true);
    }
}
