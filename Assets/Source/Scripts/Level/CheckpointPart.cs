using UnityEngine;

public class CheckpointPart : MapPart
{
    [SerializeField] private UpgradesShopTrigger _upgradesShopTrigger;
    [SerializeField] private StartLevelTrigger _startLevelTrigger;
    [SerializeField] private EndlLevelTrigger _endlLevelTrigger;

    public void Init(
        DayCycle dayCycle,
        LevelsStatisticModel levelsStatisticModel,
        CharacterUpgradesModel<SkillBehaviour> characterSkills,
        UpgradesShop upgradesShop,
        bool haveEndLevelTrigger)
    {
        _startLevelTrigger.Init(dayCycle, characterSkills);
        _upgradesShopTrigger.Init(upgradesShop);
        
        if(haveEndLevelTrigger == true)
        {
            _endlLevelTrigger.Init(dayCycle, levelsStatisticModel, characterSkills);
        }
    }
}
