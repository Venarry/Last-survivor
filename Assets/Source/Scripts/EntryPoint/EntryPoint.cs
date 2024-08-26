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

    private readonly GameTimeScaler _gameTimeScaler = new();
    private AssetsProvider _assetsProvider;
    private CharacterParametersRefresher _characterUpgradesRefresher;

    private async void Awake()
    {
        _assetsProvider = new();

        string[] loadingLabels = new string[]
        {
            "Load skills",
            "Load visual",
            "Load player",
            "Load shop",
            "Load targets",
        };

        _gameLoadingPanel.Set(loadingLabels);
        _gameLoadingPanel.ShowNext();

        UpgradesInformationDataSource skillsInformationDataSource = new();
        SpritesDataSouce spritesDataSouce = new(_assetsProvider);
        await spritesDataSouce.Load();

        _gameLoadingPanel.ShowNext();

        LevelsStatisticModel levelsStatisticModel = new();
        ItemViewFactory itemViewFactory = new(_assetsProvider, spritesDataSouce);
        await itemViewFactory.Load();
        ItemPriceFactory itemPriceFactory = new(_assetsProvider, spritesDataSouce);
        await itemPriceFactory.Load();
        MapPartsFactory mapPartsFactory = new(_assetsProvider, _upgradesShop, _dayCycle, levelsStatisticModel);
        await mapPartsFactory.Load();
        SkillsViewFactory skillsViewFactory = new(spritesDataSouce, skillsInformationDataSource, _assetsProvider);
        await skillsViewFactory.Load();

        DayCycleParameters dayCycleParameters = new();
        CoroutineProvider coroutineProvider = new GameObject("CoroutineProvider").AddComponent<CoroutineProvider>();
        _levelsStatisticView.Init(levelsStatisticModel);

        IInputProvider inputProvider = await GetInputProvider();
        TargetsProvider targetsProvider = new();

        PlayerFactory playerFactory = new(
            inputProvider,
            targetsProvider,
            _assetsProvider,
            itemViewFactory,
            skillsViewFactory,
            spritesDataSouce,
            _itemsParent,
            _skillsParent);

        _gameLoadingPanel.ShowNext();

        ExperienceModel experienceModel = new();
        CharacterUpgradesModel<SkillBehaviour> characterSkillsModel = new();
        CharacterUpgradesModel<ParametersUpgradeBehaviour> characterParametersUpgradesModel = new();
        CharacterBuffsModel characterBuffsModel = new();
        int playerHealth = 50;
        HealthModel healthModel = new(characterBuffsModel, playerHealth);
        InventoryModel inventoryModel = new();
        CharacterAttackParameters characterAttackParameters = new(characterBuffsModel);
        Player player = await playerFactory
            .Create(new(0, 0, 5), experienceModel, healthModel, characterBuffsModel, characterSkillsModel, inventoryModel, characterAttackParameters);

        player.SetBehaviour(false);

        _gameLoadingPanel.ShowNext();

        _characterUpgradesRefresher = new(levelsStatisticModel, experienceModel, characterSkillsModel, coroutineProvider);
        _characterUpgradesRefresher.Enable();
        _dayCycle.Init(dayCycleParameters, player.DayBar);

        ParameterUpgradesFactory upgradesFactory = new(characterBuffsModel);
        _upgradesShop.Init(inventoryModel, characterParametersUpgradesModel, upgradesFactory, itemPriceFactory, _gameTimeScaler);

        RoundSwordFactory roundSwordFactory = new(player.CharacterAttackParameters, _assetsProvider);

        SkillsFactory skillsFactory = new(player, targetsProvider, healthModel, characterBuffsModel, roundSwordFactory);
        _skillsOpener.Init(skillsViewFactory, characterSkillsModel, experienceModel, skillsFactory, _gameTimeScaler);

        _gameLoadingPanel.ShowNext();

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

        _targetFollower.Set(player.transform);

        LevelResourcesSpawnChance levelResourcesSpawnChance = new();

        _levelSpawner.Init(woodFactory, diamondFactory, stoneFactory, mapPartsFactory, levelResourcesSpawnChance, levelsStatisticModel);
        _mapGenerator.Init(player.transform, levelsStatisticModel, mapPartsFactory);
        _enemySpawner = new(_dayCycle, enemyFactory, levelsStatisticModel, player.Target, coroutineProvider);
        _enemySpawner.StartSpawning();

        _mapGenerator.StartGenerator();

        _gameLoadingPanel.Disable();
        player.SetBehaviour(true);

        inventoryModel.Add(LootType.Wood, 1460 + 4300);
        inventoryModel.Add(LootType.Diamond, 200 + 326);
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
