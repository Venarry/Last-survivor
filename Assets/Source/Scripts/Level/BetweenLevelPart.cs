using UnityEngine;

public class BetweenLevelPart : MapPart
{
    [SerializeField] private StartLevelTrigger _startLevelTrigger;
    [SerializeField] private EndlLevelTrigger _endlLevelTrigger;

    public void Init(
        DayCycle dayCycle,
        LevelsStatisticModel levelsStatisticModel,
        CharacterUpgradesModel<SkillBehaviour> characterSkills,
        ISaveService saveService)
    {
        _startLevelTrigger.Init(dayCycle, characterSkills);
        _endlLevelTrigger.Init(dayCycle, levelsStatisticModel, characterSkills, saveService);
    }
}