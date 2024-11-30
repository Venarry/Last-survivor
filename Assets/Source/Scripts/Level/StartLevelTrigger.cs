using Player;
using Skills;
using UnityEngine;

namespace Level
{
    public class StartLevelTrigger : MonoBehaviour
    {
        [SerializeField] private BoxCollider _startLevelCollider;

        private DayCycle.DayCycleView _dayCycle;
        private CharacterUpgradesModel<SkillBehaviour> _characterSkills;

        public void Init(DayCycle.DayCycleView dayCycle, CharacterUpgradesModel<SkillBehaviour> characterSkills)
        {
            _dayCycle = dayCycle;
            _characterSkills = characterSkills;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerCompositeRoot _))
            {
                _dayCycle.StartDayTimer();
                _characterSkills.EnableCast();
                _startLevelCollider.enabled = true;

                Destroy(this);
            }
        }
    }
}