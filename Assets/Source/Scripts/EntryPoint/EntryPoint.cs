using System.Threading.Tasks;
using Assets;
using Buffs;
using Configs;
using DataSources;
using DayCycle;
using DeathHandle;
using Experience;
using GameTutorial;
using General;
using Health;
using Inputs;
using Inventory;
using Language;
using Level;
using Level.EndLevel;
using Movers;
using ObstacleLoot;
using Player;
using Prestige;
using Save;
using Shop;
using Skills;
using Skills.Skills.Pet;
using Skills.Skills.SwordRoundAttack;
using Skills.Skills.ThrowingAxes;
using Targets;
using Targets.Enemy;
using UnityEngine;
using Upgrades;
using YG;
using YSDK;

namespace EntryPoint
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private TargetFollower _targetFollower;
        [SerializeField] private SkillsOpener _skillsOpener;
        [SerializeField] private LevelSpawner _levelSpawner;
        [SerializeField] private MapGenerator _mapGenerator;
        [SerializeField] private GameLoadingPanel _gameLoadingPanel;
        [SerializeField] private UpgradesShop _upgradesShop;
        [SerializeField] private DayCycleView _dayCycle;
        [SerializeField] private Transform _itemsParent;
        [SerializeField] private Transform _shopItemsParent;
        [SerializeField] private Transform _skillsParent;
        [SerializeField] private EnemySpawner _enemySpawner;
        [SerializeField] private LevelsStatisticView _levelsStatisticView;
        [SerializeField] private GameRestartMenu _deathMenu;
        [SerializeField] private ResetProgressHandler _resetProgressHandler;
        [SerializeField] private Tutorial _tutorial;
        [SerializeField] private EndLevelCongratulation _endLevelReward;
        [SerializeField] private TutorialSaver _tutorialSaver;

        private AssetsProvider _assetsProvider;
        private CharacterParametersRefresher _characterUpgradesRefresher;
        private LeaderboardSaver _leaderboardSaver;

        private async void Awake()
        {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
            StartGame();
#else
            await InitGameWithYandexSDK();
#endif
        }

        private async Task InitGameWithYandexSDK()
        {
            while (YandexGame.SDKEnabled == false)
            {
                await Task.Yield();
            }

            Debug.Log("SDK inited");
            StartGame();
        }

        private async void StartGame()
        {
            ChangeGameSettingsByDevice();
            CoroutineProvider coroutineProvider = new GameObject("CoroutineProvider")
                .AddComponent<CoroutineProvider>();
            StreaminAssetsReader streaminAssetsReader = new ();

            LanguageProvider languageProvider = YandexGame.lang switch
            {
                "ru" => await streaminAssetsReader.ReadAsync<LanguageProvider>("LanguageRu.json"),
                "tr" => await streaminAssetsReader.ReadAsync<LanguageProvider>("LanguageTr.json"),
                _ => await streaminAssetsReader.ReadAsync<LanguageProvider>("LanguageEn.json"),
            };

            string[] loadingLabels = new string[]
            {
                languageProvider.LoadingPart1,
                languageProvider.LoadingPart2,
                languageProvider.LoadingPart3,
                languageProvider.LoadingPart4,
            };

            _gameLoadingPanel.Set(loadingLabels);
            _gameLoadingPanel.ShowNext();

            _assetsProvider = new ();
            UpgradesInformationDataSource skillsInformationDataSource = new (languageProvider);
            PricesDataSource priceDataSource = new ();
            SpritesDataSouce spritesDataSouce = new (_assetsProvider);
            TargetsProvider<Target> targetsProvider = new ();
            TargetsProvider<Loot> lootViewProvider = new ();
            await spritesDataSouce.Load();

            LevelsStatisticModel levelsStatisticModel = new ();
            LevelResourcesSpawnChance levelResourcesSpawnChance = new ();
            CharacterUpgradesModel<SkillBehaviour> characterSkillsModel = new ();
            CharacterUpgradesModel<ParametersUpgradeBehaviour> characterParametersUpgradesModel = new ();
            CharacterUpgradesModel<ParametersUpgradeBehaviour> characterPrestigeUpgradesModel = new ();
            CharacterBuffsModel characterBuffsModel = new ();
            ExperienceModel playerExperienceModel = new (characterBuffsModel);
            DayCycleParameters dayCycleParameters = new (characterBuffsModel);
            int playerHealth = 50;
            HealthModel playerHealthModel = new (characterBuffsModel, playerHealth);
            InventoryModel inventoryModel = new ();
            CharacterAttackParameters characterAttackParameters = new (characterBuffsModel);

            IInputProvider inputProvider = await GetInputProvider();
            ItemViewFactory itemViewFactory = new (_assetsProvider, spritesDataSouce);
            await itemViewFactory.Load();
            ItemPriceFactory itemPriceFactory = new (_assetsProvider, spritesDataSouce);
            await itemPriceFactory.Load();
            SkillsViewFactory skillsViewFactory = new (spritesDataSouce, skillsInformationDataSource, _assetsProvider);
            await skillsViewFactory.Load();

            _gameLoadingPanel.ShowNext();

            PlayerFactory playerFactory = new (
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

            Vector3 spawnPosition = new (0, 0, 5);

            PlayerCompositeRoot player = await playerFactory.Create(
                position: spawnPosition,
                playerExperienceModel,
                playerHealthModel,
                characterBuffsModel,
                characterSkillsModel,
                inventoryModel,
                characterAttackParameters);

            player.SetBehaviour(false);

            _gameLoadingPanel.ShowNext();

            ParameterUpgradesFactory parametersUpgradesFactory = new (characterBuffsModel, languageProvider);

            RoundSwordFactory roundSwordFactory = new (characterAttackParameters, _assetsProvider);
            await roundSwordFactory.Load();

            ThrowingAxesFactory throwingAxesFactory = new (_assetsProvider, characterAttackParameters);
            await throwingAxesFactory.Load();

            LootFactory diamondLootFactory = new (player.LootHolder, LootType.Diamond, lootViewProvider, AssetsKeys.DiamondLoot, _assetsProvider);
            await diamondLootFactory.Load();

            TargetWithLootFactory diamondFactory = new (
                TargetType.Ore,
                levelsStatisticModel,
                targetsProvider,
                _assetsProvider,
                player.AudioSource,
                diamondLootFactory,
                AssetsKeys.Diamond);

            await diamondFactory.Load();

            LootFactory woodLootFactory = new (player.LootHolder, LootType.Wood, lootViewProvider, AssetsKeys.WoodLoot, _assetsProvider);
            await woodLootFactory.Load();

            TargetWithLootFactory woodFactory = new (
                TargetType.Wood,
                levelsStatisticModel,
                targetsProvider,
                _assetsProvider,
                player.AudioSource,
                woodLootFactory,
                AssetsKeys.Wood);
            await woodFactory.Load();

            EnemyFactory enemyFactory = new (
                TargetType.Enemy,
                targetsProvider,
                _assetsProvider,
                player.AudioSource,
                AssetsKeys.Enemy,
                attackDistance: 3);
            await enemyFactory.Load();

            TargetFactory stoneFactory = new (TargetType.Ore, targetsProvider, _assetsProvider, player.AudioSource, AssetsKeys.Stone);
            await stoneFactory.Load();

            PetFactory petFactory = new (_assetsProvider, characterAttackParameters, characterBuffsModel, player.TargetSearcher, player.transform);
            await petFactory.Load();

            SkillsFactory skillsFactory = new (
                coroutineProvider,
                player,
                targetsProvider,
                playerHealthModel,
                characterBuffsModel,
                roundSwordFactory,
                throwingAxesFactory,
                petFactory,
                languageProvider);

            _gameLoadingPanel.ShowNext();

            ProgressHandler progressHandler = new (
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

            if (YandexGame.auth == true)
            {
                progressHandler.LoadFromCloud(YandexGame.savesData.ProgressData);
            }
            else
            {
                progressHandler.LoadFromLocal();
            }

            MapPartsFactory mapPartsFactory = new (
                _assetsProvider,
                _upgradesShop,
                _dayCycle,
                levelsStatisticModel,
                characterSkillsModel,
                _endLevelReward,
                progressHandler);

            await mapPartsFactory.Load();

            _upgradesShop.Init(
                priceDataSource,
                inventoryModel,
                characterParametersUpgradesModel,
                characterPrestigeUpgradesModel,
                parametersUpgradesFactory,
                itemPriceFactory);
            _skillsOpener.Init(skillsViewFactory, characterSkillsModel, playerExperienceModel, skillsFactory);
            _levelSpawner.Init(woodFactory, diamondFactory, stoneFactory, mapPartsFactory, levelResourcesSpawnChance);
            _mapGenerator.Init(player.transform, levelsStatisticModel, mapPartsFactory);
            _deathMenu.Init(
                characterSkillsModel,
                playerExperienceModel,
                player.ThirdPersonMovement,
                levelsStatisticModel,
                playerHealthModel,
                lootViewProvider,
                progressHandler,
                spawnPosition);
            _enemySpawner = new (_dayCycle, enemyFactory, levelsStatisticModel, player.Target, coroutineProvider);
            _levelsStatisticView.Init(levelsStatisticModel);
            _characterUpgradesRefresher = new (
                levelsStatisticModel,
                playerExperienceModel,
                playerHealthModel,
                characterSkillsModel,
                coroutineProvider);
            _dayCycle.Init(dayCycleParameters, player.DayUIParent, player.DayBar, player.DayTimeLabel);
            _resetProgressHandler.Init(
                levelsStatisticModel,
                inventoryModel,
                characterParametersUpgradesModel,
                player.ThirdPersonMovement,
                _targetFollower,
                progressHandler,
                languageProvider,
                spawnPosition);

            _upgradesShop.InitButtons();
            _targetFollower.Set(player.transform);
            _characterUpgradesRefresher.Enable();
            _levelsStatisticView.SpawnLevelsIcon();
            _enemySpawner.StartSpawning();
            _mapGenerator.StartGenerator();

            if (progressHandler.TutorialPassed == false)
            {
                _tutorial.InitBase();
                _tutorial.InitMovement(player.ThirdPersonMovement);
                _tutorialSaver.Init(progressHandler);

                _mapGenerator.CheckpointZoneSpawned += OnCheckpointZoneSpawn;
            }

            _leaderboardSaver = new (levelsStatisticModel, progressHandler);
            _leaderboardSaver.Enable();

            _gameLoadingPanel.Disable();
            player.SetBehaviour(true);

            YandexGame.GameReadyAPI();
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
                MobileInputsProviderFactory mobileInputsProviderFactory = new (_assetsProvider);
                return await mobileInputsProviderFactory.Create(_canvas.transform);
            }
        }

        private void ChangeGameSettingsByDevice()
        {
            bool isMobile = Application.isMobilePlatform;

            if (isMobile == true)
            {
                _camera.fieldOfView = 55;
            }
        }

        private void OnDestroy()
        {
            _assetsProvider.Clear();
            _characterUpgradesRefresher.Disable();
            _enemySpawner.DisableSpawning();
            GameTimeScaler.RemoveAll();
            _leaderboardSaver.Disable();
        }
    }
}