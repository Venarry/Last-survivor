using System.Threading.Tasks;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private TargetFollower _targetFollower;
    [SerializeField] private SkillsOpener _skillsOpener;
    [SerializeField] private LevelSpawner _levelSpawner;
    [SerializeField] private MapGenerator _mapGenerator;
    [SerializeField] private GameLoadingPanel _gameLoadingPanel;
    [SerializeField] private UpgradesShop _upgradesShop;
    [SerializeField] private DayCycle _dayCycle;
    [SerializeField] private Transform _itemsParent;
    [SerializeField] private Transform _skillsParent;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private LevelsStatisticView _levelsStatisticView;

    [SerializeField] private PetBehaviour _petBehaviour;

    private readonly GameTimeScaler _gameTimeScaler = new();
    private AssetsProvider _assetsProvider;
    private CharacterParametersRefresher _characterUpgradesRefresher;

    private async void Awake()
    {
        _assetsProvider = new();

        string[] loadingLabels = new string[]
        {
            "Load map",
            "Load player",
            "Load shop",
            "Load targets",
        };

        CoroutineProvider coroutineProvider = new GameObject("CoroutineProvider").AddComponent<CoroutineProvider>();

        _gameLoadingPanel.Set(loadingLabels);
        _gameLoadingPanel.ShowNext();

        UpgradesInformationDataSource skillsInformationDataSource = new();
        SpritesDataSouce spritesDataSouce = new(_assetsProvider);
        await spritesDataSouce.Load();

        DayCycleParameters dayCycleParameters = new();
        LevelsStatisticModel levelsStatisticModel = new();
        ExperienceModel experienceModel = new();
        CharacterUpgradesModel<SkillBehaviour> characterSkillsModel = new();
        CharacterUpgradesModel<ParametersUpgradeBehaviour> characterParametersUpgradesModel = new();
        CharacterBuffsModel characterBuffsModel = new();
        int playerHealth = 50;
        HealthModel playerHealthModel = new(characterBuffsModel, playerHealth);
        InventoryModel inventoryModel = new();
        CharacterAttackParameters characterAttackParameters = new(characterBuffsModel);

        IInputProvider inputProvider = await GetInputProvider();
        TargetsProvider targetsProvider = new();
        ItemViewFactory itemViewFactory = new(_assetsProvider, spritesDataSouce);
        await itemViewFactory.Load();
        ItemPriceFactory itemPriceFactory = new(_assetsProvider, spritesDataSouce);
        await itemPriceFactory.Load();
        SkillsViewFactory skillsViewFactory = new(spritesDataSouce, skillsInformationDataSource, _assetsProvider);
        await skillsViewFactory.Load();

        _gameLoadingPanel.ShowNext();

        PlayerFactory playerFactory = new(
            inputProvider,
            targetsProvider,
            _assetsProvider,
            itemViewFactory,
            skillsViewFactory,
            spritesDataSouce,
            _itemsParent,
            _skillsParent);

        Player player = await playerFactory.Create(
            position: new(0, 0, 5),
            experienceModel,
            playerHealthModel,
            characterBuffsModel,
            characterSkillsModel,
            inventoryModel,
            characterAttackParameters);

        player.SetBehaviour(false);

        ProgressHandler progressHandler = new(inventoryModel, levelsStatisticModel, characterParametersUpgradesModel);
        progressHandler.Load();

        _gameLoadingPanel.ShowNext();
        
        _dayCycle.Init(dayCycleParameters, player.DayBar);

        ParameterUpgradesFactory upgradesFactory = new(characterBuffsModel);
        _upgradesShop.Init(inventoryModel, characterParametersUpgradesModel, upgradesFactory, itemPriceFactory, _gameTimeScaler);

        RoundSwordFactory roundSwordFactory = new(characterAttackParameters, _assetsProvider);
        roundSwordFactory.Load();

        ThrowingAxesFactory throwingAxesFactory = new(_assetsProvider, characterAttackParameters);
        throwingAxesFactory.Load();

        DiamondLootFactory diamondLootFactory = new(player.LootHolder, _assetsProvider);
        await diamondLootFactory.Load();

        DiamondFactory diamondFactory = new(levelsStatisticModel, targetsProvider, _assetsProvider, diamondLootFactory);
        await diamondFactory.Load();

        WoodLootFactory woodLootFactory = new(player.LootHolder, _assetsProvider);
        await woodLootFactory.Load();

        WoodFactory woodFactory = new(levelsStatisticModel, targetsProvider, _assetsProvider, woodLootFactory);
        await woodFactory.Load();

        EnemyFactory enemyFactory = new(targetsProvider, _assetsProvider, attackDistance: 3);
        await enemyFactory.Load();

        StoneFactory stoneFactory = new(targetsProvider, _assetsProvider);
        await stoneFactory.Load();

        PetFactory petFactory = new(_assetsProvider, characterAttackParameters, characterBuffsModel, player.TargetSearcher, player.transform);
        await petFactory.Load();

        SkillsFactory skillsFactory = new(
            coroutineProvider, player, targetsProvider, playerHealthModel, characterBuffsModel, roundSwordFactory, throwingAxesFactory, petFactory);

        _gameLoadingPanel.ShowNext();

        LevelResourcesSpawnChance levelResourcesSpawnChance = new();

        MapPartsFactory mapPartsFactory = new(_assetsProvider, _upgradesShop, _dayCycle, levelsStatisticModel, characterSkillsModel, playerHealthModel);
        await mapPartsFactory.Load();

        _skillsOpener.Init(skillsViewFactory, characterSkillsModel, experienceModel, skillsFactory, _gameTimeScaler);
        _levelSpawner.Init(woodFactory, diamondFactory, stoneFactory, mapPartsFactory, levelResourcesSpawnChance, levelsStatisticModel);
        _mapGenerator.Init(player.transform, levelsStatisticModel, mapPartsFactory);
        _enemySpawner = new(_dayCycle, enemyFactory, levelsStatisticModel, player.Target, coroutineProvider);
        _levelsStatisticView.Init(levelsStatisticModel);
        _characterUpgradesRefresher = new(levelsStatisticModel, experienceModel, playerHealthModel, characterSkillsModel, coroutineProvider);

        _targetFollower.Set(player.transform);
        _characterUpgradesRefresher.Enable();
        _levelsStatisticView.SpawnLevelsIcon();
        _enemySpawner.StartSpawning();
        _mapGenerator.StartGenerator();

        _gameLoadingPanel.Disable();
        player.SetBehaviour(true);

        //inventoryModel.Add(LootType.Wood, 1460 + 4300);
        //inventoryModel.Add(LootType.Diamond, 200 + 326);

        //characterSkillsModel.Add(skillsFactory.CreateAttackSpeedSkill());
    }

    private async Task<IInputProvider> GetInputProvider()
    {
        bool isMobile = Application.isMobilePlatform;

        if (isMobile == false)
        {
            return new KeyboardInputProvider();
        }
        else
        {
            MobileInputsProviderFactory mobileInputsProviderFactory = new(_assetsProvider);
            return await mobileInputsProviderFactory.Create(_canvas.transform);
        }
    }

    private void OnDestroy()
    {
        _assetsProvider.Clear();
        _characterUpgradesRefresher.Disable();
        _enemySpawner.DisableSpawning();
        _gameTimeScaler.RemoveAll();
    }
}
