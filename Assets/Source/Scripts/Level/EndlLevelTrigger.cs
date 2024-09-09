using UnityEngine;

public class EndlLevelTrigger : MonoBehaviour
{
    [SerializeField] private BoxCollider _endLevelCollider;

    private LevelsStatisticModel _levelsStatisticModel;
    private DayCycle _dayCycle;
    private CharacterUpgradesModel<SkillBehaviour> _characterSkills;
    private HealthModel _playerHealthModel;

    public void Init(
        DayCycle dayCycle,
        LevelsStatisticModel levelsStatisticModel,
        CharacterUpgradesModel<SkillBehaviour> characterSkills,
        HealthModel playerHealthModel)
    {
        _dayCycle = dayCycle;
        _levelsStatisticModel = levelsStatisticModel;
        _characterSkills = characterSkills;
        _playerHealthModel = playerHealthModel;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player _))
        {
            _levelsStatisticModel.Add();
            _dayCycle.ResetTime();
            _characterSkills.DisableCast();
            _playerHealthModel.Restore();

            _endLevelCollider.enabled = true;
            Destroy(this);
        }
    }
}
