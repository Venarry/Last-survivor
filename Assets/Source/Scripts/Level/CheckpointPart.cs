﻿using UnityEngine;

public class CheckpointPart : MapPart
{
    [SerializeField] private StartLevelTrigger _startLevelTrigger;
    [SerializeField] private EndlLevelTrigger _endlLevelTrigger;
    [SerializeField] private BarrierModelEnabler _barrierModelEnabler;

    [field: SerializeField] public UpgradesShopTrigger UpgradesShopTrigger { get; private set; }
    [field: SerializeField] public GameObject ShopPoint { get; private set; }

    public void Init(
        DayCycle dayCycle,
        LevelsStatisticModel levelsStatisticModel,
        CharacterUpgradesModel<SkillBehaviour> characterSkills,
        UpgradesShop upgradesShop,
        EndLevelCongratulation endLevelReward,
        IProgressSaveService saveService,
        bool haveEndLevelTrigger)
    {
        _startLevelTrigger.Init(dayCycle, characterSkills);
        UpgradesShopTrigger.Init(upgradesShop);
        _endlLevelTrigger.Init(dayCycle, levelsStatisticModel, characterSkills, endLevelReward, saveService);

        if (haveEndLevelTrigger == false)
        {
            _endlLevelTrigger.gameObject.SetActive(false);
            _barrierModelEnabler.gameObject.SetActive(false);
        }
    }
}
