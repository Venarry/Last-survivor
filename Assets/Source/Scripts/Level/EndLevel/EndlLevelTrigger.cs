using DayCycle;
using Player;
using Save;
using Skills;
using UnityEngine;

namespace Level.EndLevel
{
    public class EndlLevelTrigger : MonoBehaviour
    {
        [SerializeField] private BoxCollider _endLevelCollider;

        private LevelsStatisticModel _levelsStatisticModel;
        private DayCycleView _dayCycle;
        private CharacterUpgradesModel<SkillBehaviour> _characterSkills;
        private EndLevelCongratulation _endLevelReward;
        private IProgressSaveService _saveService;

        public void Init(
            DayCycleView dayCycle,
            LevelsStatisticModel levelsStatisticModel,
            CharacterUpgradesModel<SkillBehaviour> characterSkills,
            EndLevelCongratulation endLevelReward,
            IProgressSaveService saveService)
        {
            _dayCycle = dayCycle;
            _levelsStatisticModel = levelsStatisticModel;
            _characterSkills = characterSkills;
            _endLevelReward = endLevelReward;
            _saveService = saveService;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerCompositeRoot _))
            {
                _levelsStatisticModel.Add();
                _dayCycle.ResetTime();
                _characterSkills.DisableCast();
                _saveService.Save();
                _endLevelReward.ShowMenu();

                _endLevelCollider.enabled = true;
                Destroy(this);
            }
        }
    }
}