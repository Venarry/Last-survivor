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
    [SerializeField] private Transform _itemsParent;
    [SerializeField] private Transform _skillsParent;

    private AssetsProvider _assetsProvider;

    private async void Awake()
    {
        _assetsProvider = new();

        string[] labels = new string[]
        {
            "Load skills",
            "Load player",
            "Load factorys",
        };

        _gameLoadingPanel.Set(labels);
        _gameLoadingPanel.ShowNext();

        SkillsInformationDataSource skillsInformationDataSource = new();
        SpritesDataSouce spritesDataSouce = new(_assetsProvider);
        await spritesDataSouce.Load();

        ItemViewFactory itemViewFactory = new(_assetsProvider, spritesDataSouce);

        LevelResourcesSpawnChance levelResourcesSpawnChance = new();
        LevelsStatisticModel levelsStatistic = new();

        SkillsViewFactory skillsViewFactory = new(spritesDataSouce, skillsInformationDataSource, _assetsProvider);

        IInputProvider inputProvider = await GetInputProvider();
        TargetsProvider targetsProvider = new();

        PlayerFactory playerFactory = new(inputProvider, targetsProvider, _assetsProvider, itemViewFactory, skillsViewFactory, spritesDataSouce, _itemsParent, _skillsParent);

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

        UpgradesFactory upgradesFactory = new(characterAttackParameters);
        _upgradesShop.Init(inventoryModel, characterUpgrades, upgradesFactory);

        RoundSwordFactory roundSwordFactory = new(player.CharacterAttackParameters, _assetsProvider);

        SkillsFactory skillsFactory = new(player, targetsProvider, healthModel, roundSwordFactory);
        _skillsOpener.Init(skillsViewFactory, characterSkillsModel, experienceModel, skillsFactory);

        _gameLoadingPanel.ShowNext();

        DiamondLootFactory diamondLootFactory = new(player.LootHolder, _assetsProvider);
        await diamondLootFactory.Load();

        DiamondFactory diamondFactory = new(targetsProvider, _assetsProvider, diamondLootFactory);
        await diamondFactory.Load();

        WoodLootFactory woodLootFactory = new(player.LootHolder, _assetsProvider);
        await woodLootFactory.Load();

        WoodFactory woodFactory = new(targetsProvider, _assetsProvider, woodLootFactory);
        await woodFactory.Load();

        EnemyFactory enemyFactory = new(targetsProvider, _assetsProvider);
        await enemyFactory.Load();

        StoneFactory stoneFactory = new(targetsProvider, _assetsProvider);
        await stoneFactory.Load();

        _targetFollower.Set(player.transform);

        _levelSpawner.Init(woodFactory, diamondFactory, stoneFactory, levelResourcesSpawnChance, levelsStatistic);

        _mapGenerator.Init(player.transform, levelsStatistic);
        _mapGenerator.StartGenerator();

        _gameLoadingPanel.Disable();
        player.SetBehaviour(true);
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
    }
}
