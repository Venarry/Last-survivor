using System.Collections;
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
    [SerializeField] private Transform _shopItemsParent;
    [SerializeField] private Transform _skillsParent;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private LevelsStatisticView _levelsStatisticView;
    [SerializeField] private GameRestartMenu _deathMenu;
    [SerializeField] private ResetProgressHandler _resetProgressHandler;
    [SerializeField] private Tutorial _tutorial;

    private readonly GameTimeScaler _gameTimeScaler = new();
    private AssetsProvider _assetsProvider;
    private CharacterParametersRefresher _characterUpgradesRefresher;

    private IEnumerator InitYandexSDK()
    {
        yield return null;//YandexGamesSdk.Initialize();
        Debug.Log("SDK inited");
    }

    private async void Awake()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        StartCoroutine(InitYandexSDK());
#endif
        string[] loadingLabels = new string[]
        {
            "Load map",
            "Load player",
            "Load shop",
            "Load targets",
        };

        _gameLoadingPanel.Set(loadingLabels);
        _gameLoadingPanel.ShowNext();

        _assetsProvider = new();
        CoroutineProvider coroutineProvider = new GameObject("CoroutineProvider").AddComponent<CoroutineProvider>();
        UpgradesInformationDataSource skillsInformationDataSource = new();
        PricesDataSource priceDataSource = new();
        SpritesDataSouce spritesDataSouce = new(_assetsProvider);
        TargetsProvider<Target> targetsProvider = new();
        TargetsProvider<Loot> lootViewProvider = new();
        await spritesDataSouce.Load();

        LevelsStatisticModel levelsStatisticModel = new();
        LevelResourcesSpawnChance levelResourcesSpawnChance = new();
        CharacterUpgradesModel<SkillBehaviour> characterSkillsModel = new();
        CharacterUpgradesModel<ParametersUpgradeBehaviour> characterParametersUpgradesModel = new();
        CharacterUpgradesModel<ParametersUpgradeBehaviour> characterPrestigeUpgradesModel = new();
        CharacterBuffsModel characterBuffsModel = new();
        ExperienceModel playerExperienceModel = new(characterBuffsModel);
        DayCycleParameters dayCycleParameters = new(characterBuffsModel);
        int playerHealth = 50;
        HealthModel playerHealthModel = new(characterBuffsModel, playerHealth);
        InventoryModel inventoryModel = new();
        CharacterAttackParameters characterAttackParameters = new(characterBuffsModel);

        IInputProvider inputProvider = await GetInputProvider();
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
            _shopItemsParent,
            _skillsParent,
            _deathMenu);

        Vector3 spawnPosition = new(0, 0, 5);

        Player player = await playerFactory.Create(
            position: spawnPosition,
            playerExperienceModel,
            playerHealthModel,
            characterBuffsModel,
            characterSkillsModel,
            inventoryModel,
            characterAttackParameters);

        player.SetBehaviour(false);

        _gameLoadingPanel.ShowNext();

        ParameterUpgradesFactory parametersUpgradesFactory = new(characterBuffsModel);

        RoundSwordFactory roundSwordFactory = new(characterAttackParameters, _assetsProvider);
        await roundSwordFactory.Load();

        ThrowingAxesFactory throwingAxesFactory = new(_assetsProvider, characterAttackParameters);
        await throwingAxesFactory.Load();

        DiamondLootFactory diamondLootFactory = new(player.LootHolder, lootViewProvider, _assetsProvider);
        await diamondLootFactory.Load();

        DiamondFactory diamondFactory = new(levelsStatisticModel, targetsProvider, _assetsProvider, diamondLootFactory);
        await diamondFactory.Load();

        WoodLootFactory woodLootFactory = new(player.LootHolder, lootViewProvider, _assetsProvider);
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
            coroutineProvider,
            player,
            targetsProvider,
            playerHealthModel,
            characterBuffsModel,
            roundSwordFactory,
            throwingAxesFactory,
            petFactory);

        _gameLoadingPanel.ShowNext();

        ProgressHandler progressHandler = new(
            inventoryModel,
            playerHealthModel,
            playerExperienceModel,
            levelsStatisticModel,
            characterParametersUpgradesModel,
            characterPrestigeUpgradesModel,
            characterSkillsModel,
            skillsFactory,
            parametersUpgradesFactory,
            _upgradesShop);

        progressHandler.Load();

        inventoryModel.Add(LootType.Wood, 5760);
        inventoryModel.Add(LootType.Diamond, 526);
        inventoryModel.Add(LootType.Prestige, 500);

        MapPartsFactory mapPartsFactory = new(
            _assetsProvider, _upgradesShop, _dayCycle, levelsStatisticModel, characterSkillsModel, playerHealthModel, progressHandler);
        await mapPartsFactory.Load();

        _upgradesShop.Init(priceDataSource, inventoryModel, characterParametersUpgradesModel, characterPrestigeUpgradesModel, parametersUpgradesFactory, itemPriceFactory, _gameTimeScaler);
        _skillsOpener.Init(skillsViewFactory, characterSkillsModel, playerExperienceModel, skillsFactory, _gameTimeScaler);
        _levelSpawner.Init(woodFactory, diamondFactory, stoneFactory, mapPartsFactory, levelResourcesSpawnChance);
        _mapGenerator.Init(player.transform, levelsStatisticModel, mapPartsFactory);
        _deathMenu.Init(characterSkillsModel, playerExperienceModel, player.ThirdPersonMovement, levelsStatisticModel, playerHealthModel, lootViewProvider, progressHandler, spawnPosition);
        _enemySpawner = new(_dayCycle, enemyFactory, levelsStatisticModel, player.Target, coroutineProvider);
        _levelsStatisticView.Init(levelsStatisticModel);
        _characterUpgradesRefresher = new(levelsStatisticModel, playerExperienceModel, playerHealthModel, characterSkillsModel, coroutineProvider);
        _dayCycle.Init(dayCycleParameters, player.DayUIParent, player.DayBar, player.DayTimeLabel);
        _resetProgressHandler.Init(levelsStatisticModel, inventoryModel, characterParametersUpgradesModel, player.ThirdPersonMovement, progressHandler, spawnPosition);

        _upgradesShop.InitButtons();
        _targetFollower.Set(player.transform);
        _characterUpgradesRefresher.Enable();
        _levelsStatisticView.SpawnLevelsIcon();
        _enemySpawner.StartSpawning();
        _mapGenerator.StartGenerator();

        _gameLoadingPanel.Disable();
        player.SetBehaviour(true);

        //_tutorial.InitBase(_gameTimeScaler);
        //_tutorial.InitMovement(player.ThirdPersonMovement);

        //_mapGenerator.CheckpointZoneSpawned += OnCheckpointZoneSpawn;
    }

    private void OnCheckpointZoneSpawn(CheckpointPart part)
    {
        _mapGenerator.CheckpointZoneSpawned -= OnCheckpointZoneSpawn;
        _tutorial.InitGoToShop(part.UpgradesShopTrigger, part.ShopPoint);
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
