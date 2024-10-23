using System.Threading.Tasks;
using UnityEngine;

public class MapPartsFactory
{
    private readonly AssetsProvider _assetsProvider;
    private readonly UpgradesShop _upgradesShop;
    private readonly DayCycle _dayCycle;
    private readonly LevelsStatisticModel _levelsStatisticModel;
    private readonly CharacterUpgradesModel<SkillBehaviour> _characterSkills;
    private readonly HealthModel _playerHealthModel;
    private readonly EndLevelReward _endLevelReward;
    private readonly IProgressSaveService _saveService;

    public MapPartsFactory(
        AssetsProvider assetsProvider,
        UpgradesShop upgradesShop,
        DayCycle dayCycle,
        LevelsStatisticModel levelsStatisticModel,
        CharacterUpgradesModel<SkillBehaviour> characterSkills,
        HealthModel playerHealthModel,
        EndLevelReward endLevelReward,
        IProgressSaveService saveService)
    {
        _assetsProvider = assetsProvider;
        _upgradesShop = upgradesShop;
        _dayCycle = dayCycle;
        _levelsStatisticModel = levelsStatisticModel;
        _characterSkills = characterSkills;
        _playerHealthModel = playerHealthModel;
        _endLevelReward = endLevelReward;
        _saveService = saveService;
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

    public async Task<CheckpointPart> CreateCheckPointZone(Vector3 spawnPosition, bool haveEndLevelTrigger)
    {
        CheckpointPart part = Object
            .Instantiate(await _assetsProvider.LoadGameObject<CheckpointPart>(AssetsKeys.CheckpointZone), spawnPosition, Quaternion.identity);

        part.Init(_dayCycle, _levelsStatisticModel, _characterSkills, _upgradesShop, _endLevelReward, _saveService, haveEndLevelTrigger);

        return part;
    }

    public async Task<BetweenLevelPart> CreateBetweenLevelZone(Vector3 spawnPosition, bool haveEndLevelTrigger) 
    {
        BetweenLevelPart part = Object
            .Instantiate(await _assetsProvider.LoadGameObject<BetweenLevelPart>(AssetsKeys.BetweenLevelsZone), spawnPosition, Quaternion.identity);

        part.Init(_dayCycle, _levelsStatisticModel, _characterSkills, _endLevelReward, _saveService, haveEndLevelTrigger);

        return part;
    }
}