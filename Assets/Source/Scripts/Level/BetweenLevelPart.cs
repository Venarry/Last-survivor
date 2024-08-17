using UnityEngine;

public class BetweenLevelPart : MapPart
{
    [SerializeField] private StartLevelTrigger _startLevelTrigger;
    [SerializeField] private EndlLevelTrigger _endlLevelTrigger;

    public void Init(DayCycle dayCycle, LevelsStatisticModel levelsStatisticModel)
    {
        _startLevelTrigger.Init(dayCycle);
        _endlLevelTrigger.Init(dayCycle, levelsStatisticModel);
    }
}