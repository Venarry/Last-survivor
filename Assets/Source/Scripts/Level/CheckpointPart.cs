using UnityEngine;

public class CheckpointPart : MapPart
{
    [SerializeField] private UpgradesShopTrigger _upgradesShopTrigger;
    [SerializeField] private StartLevelTrigger _startLevelTrigger;
    [SerializeField] private EndlLevelTrigger _endlLevelTrigger;

    public void Init(DayCycle dayCycle, LevelsStatisticModel levelsStatisticModel, UpgradesShop upgradesShop, bool haveEndLevelTrigger)
    {
        _startLevelTrigger.Init(dayCycle);
        _upgradesShopTrigger.Init(upgradesShop);
        
        if(haveEndLevelTrigger == true)
        {
            _endlLevelTrigger.Init(dayCycle, levelsStatisticModel);
        }
    }
}
