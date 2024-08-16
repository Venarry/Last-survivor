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
    [SerializeField] private DayCycleView _dayCycleView;
    [SerializeField] private Transform _itemsParent;
    [SerializeField] private Transform _skillsParent;
    [SerializeField] private EnemySpawner _enemySpawner;

    private AssetsProvider _assetsProvider;

    private async void Awake()
    {
        _assetsProvider = new();

        string[] labels = new string[]
        {
            "Load skills",
            "Load map",
            "Load player",
            "Load shop",
            "Load targets",
        };

        _gameLoadingPanel.Set(labels);
        _gameLoadingPanel.ShowNext();

        SkillsInformationDataSource skillsInformationDataSource = new();
        SpritesDataSouce spritesDataSouce = new(_assetsProvider);
        await spritesDataSouce.Load();

        _gameLoadingPanel.ShowNext();

        ItemViewFactory itemViewFactory = new(_assetsProvider, spritesDataSouce);
        ItemPriceFactory itemPriceFactory = new(_assetsProvider, spritesDataSouce);
        MapPartsFactory mapPartsFactory = new(_assetsProvider, _upgradesShop);
        await mapPartsFactory.Load();
        SkillsViewFactory skillsViewFactory = new(spritesDataSouce, skillsInformationDataSource, _assetsProvider);
        DayCycleParameters dayCycleParameters = new();

        LevelsStatisticModel levelsStatisticModel = new();

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
        CharacterSkillsModel characterSkillsModel = new();
        CharacterUpgrades characterUpgrades = new();
        int playerHealth = 30;
        HealthModel healthModel = new(playerHealth);
        InventoryModel inventoryModel = new();
        CharacterAttackParameters characterAttackParameters = new();
        Player player = await playerFactory
            .Create(new(0, 0, 5), experienceModel, healthModel, characterSkillsModel, inventoryModel, characterAttackParameters);

        player.SetBehaviour(false);

        _gameLoadingPanel.ShowNext();

        _dayCycleView.Init(dayCycleParameters, player.DayBar);

        UpgradesFactory upgradesFactory = new(characterAttackParameters);
        _upgradesShop.Init(inventoryModel, characterUpgrades, upgradesFactory, itemPriceFactory);

        RoundSwordFactory roundSwordFactory = new(player.CharacterAttackParameters, _assetsProvider);

        SkillsFactory skillsFactory = new(player, targetsProvider, healthModel, roundSwordFactory);
        _skillsOpener.Init(skillsViewFactory, characterSkillsModel, experienceModel, skillsFactory);

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

        _levelSpawner.Init(woodFactory, diamondFactory, stoneFactory, levelResourcesSpawnChance, levelsStatisticModel);
        _mapGenerator.Init(player.transform, levelsStatisticModel, mapPartsFactory);
        _enemySpawner.Init(enemyFactory, levelsStatisticModel, player.Target);

        _mapGenerator.StartGenerator();

        _gameLoadingPanel.Disable();
        player.SetBehaviour(true);

        //inventoryModel.Add(LootType.Diamond, 10000);
        //inventoryModel.Add(LootType.Wood, 10000);
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            _dayCycleView.StartDayTimer();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            _dayCycleView.EndNight();
        }
    }

    private void OnDestroy()
    {
        _assetsProvider.Clear();
    }
}
