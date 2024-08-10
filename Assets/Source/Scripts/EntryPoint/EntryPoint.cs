using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private TargetFollower _targetFollower;
    [SerializeField] private SkillsOpener _skillsOpener;
    [SerializeField] private LevelSpawner _levelSpawner;
    [SerializeField] private MapGenerator _mapGenerator;
    [SerializeField] private GameLoadingPanel _gameLoadingPanel;

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
        SkillsSpriteDataSouce skillsSpriteDataSouce = new(_assetsProvider);
        await skillsSpriteDataSouce.Load();

        LevelResourcesSpawnChance levelResourcesSpawnChance = new();
        LevelsStatistic levelsStatistic = new();

        SkillToChooseFactory skillToChooseFactory = new(skillsSpriteDataSouce, skillsInformationDataSource, _assetsProvider);

        IInputProvider inputProvider = await GetInputProvider();
        TargetsProvider targetsProvider = new();

        PlayerFactory playerFactory = new(inputProvider, targetsProvider, _assetsProvider);

        _gameLoadingPanel.ShowNext();

        ExperienceModel experienceModel = new();
        int playerHealth = 30;
        HealthModel healthModel = new(playerHealth);
        Player player = await playerFactory.Create(new(0, 0, 5), experienceModel, healthModel);
        player.SetBehaviour(false);

        RoundSwordFactory roundSwordFactory = new(player.CharacterAttackParameters, _assetsProvider);

        SkillsFactory skillsFactory = new(player, targetsProvider, healthModel, roundSwordFactory);
        _skillsOpener.Init(skillToChooseFactory, player.CharacterSkills, experienceModel, skillsFactory);

        CharacterUpgrades characterUpgrades = new();
        //characterUpgrades.Add(new EnemyDamageUpgrade(player.CharacterAttackParameters));

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(0);
        }
    }

    private void OnDestroy()
    {
        _assetsProvider.Clear();
    }
}
