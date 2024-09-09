using UnityEngine;

public class StartLevelTrigger : MonoBehaviour
{
    [SerializeField] private BoxCollider _startLevelCollider;

    private DayCycle _dayCycle;
    private CharacterUpgradesModel<SkillBehaviour> _characterSkills;

    public void Init(DayCycle dayCycle, CharacterUpgradesModel<SkillBehaviour> characterSkills)
    {
        _dayCycle = dayCycle;
        _characterSkills = characterSkills;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player _))
        {
            _dayCycle.StartDayTimer();
            _characterSkills.EnableCast();
            _startLevelCollider.enabled = true;

            Destroy(this);
        }
    }
}