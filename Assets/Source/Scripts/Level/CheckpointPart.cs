using UnityEngine;

public class CheckpointPart : MapPart
{
    [SerializeField] private UpgradesShopTrigger _upgradesShopTrigger;
    [SerializeField] private StartLevelTrigger _startLevelTrigger;
    [SerializeField] private EndlLevelTrigger _endlLevelTrigger;
    [SerializeField] private BarrierModelEnabler _barrierModelEnabler;

    public void Init(
        DayCycle dayCycle,
        LevelsStatisticModel levelsStatisticModel,
        CharacterUpgradesModel<SkillBehaviour> characterSkills,
        UpgradesShop upgradesShop,
        bool haveEndLevelTrigger)
    {
        _startLevelTrigger.Init(dayCycle, characterSkills);
        _upgradesShopTrigger.Init(upgradesShop);
        _endlLevelTrigger.Init(dayCycle, levelsStatisticModel, characterSkills);

        if (haveEndLevelTrigger == false)
        {
            //_endlLevelTrigger.Init(dayCycle, levelsStatisticModel, characterSkills, playerHealthModel);
            _endlLevelTrigger.gameObject.SetActive(false);
            _barrierModelEnabler.gameObject.SetActive(false);
        }
    }
}
