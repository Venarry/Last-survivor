﻿using System.Threading.Tasks;
using UnityEngine;

public class MapPartsFactory
{
    private readonly AssetsProvider _assetsProvider;
    private readonly UpgradesShop _upgradesShop;
    private readonly DayCycle _dayCycle;
    private readonly LevelsStatisticModel _levelsStatisticModel;
    private readonly CharacterUpgradesModel<SkillBehaviour> _characterSkills;
    private readonly HealthModel _playerHealthModel;

    public MapPartsFactory(
        AssetsProvider assetsProvider,
        UpgradesShop upgradesShop,
        DayCycle dayCycle,
        LevelsStatisticModel levelsStatisticModel,
        CharacterUpgradesModel<SkillBehaviour> characterSkills,
        HealthModel playerHealthModel)
    {
        _assetsProvider = assetsProvider;
        _upgradesShop = upgradesShop;
        _dayCycle = dayCycle;
        _levelsStatisticModel = levelsStatisticModel;
        _characterSkills = characterSkills;
        _playerHealthModel = playerHealthModel;
    }

    public async Task Load()
    {
        await _assetsProvider.LoadGameObject<MapPart>(AssetsKeys.LevelZone);
        await _assetsProvider.LoadGameObject<CheckpointPart>(AssetsKeys.CheckpointZone);
        await _assetsProvider.LoadGameObject<BetweenLevelPart>(AssetsKeys.BetweenLevelsZone);
    }

    public async Task<MapPart> CreateLevelZone(Vector3 spawnPosition)
    {
        MapPart part = Object
            .Instantiate(await _assetsProvider.LoadGameObject<MapPart>(AssetsKeys.LevelZone), spawnPosition, Quaternion.identity);

        return part;
    }

    public async Task<MapPart> CreateCheckPointZone(Vector3 spawnPosition, bool haveEndLevelTrigger)
    {
        CheckpointPart part = Object
            .Instantiate(await _assetsProvider.LoadGameObject<CheckpointPart>(AssetsKeys.CheckpointZone), spawnPosition, Quaternion.identity);

        part.Init(_dayCycle, _levelsStatisticModel, _characterSkills, _upgradesShop, haveEndLevelTrigger);

        return part;
    }

    public async Task<MapPart> CreateBetweenLevelZone(Vector3 spawnPosition) 
    {
        BetweenLevelPart part = Object
            .Instantiate(await _assetsProvider.LoadGameObject<BetweenLevelPart>(AssetsKeys.BetweenLevelsZone), spawnPosition, Quaternion.identity);

        part.Init(_dayCycle, _levelsStatisticModel, _characterSkills);

        return part;
    }
}