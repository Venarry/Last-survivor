using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private TargetFollower _targetFollower;
    [SerializeField] private SkillsOpener _skillsOpener;

    private AssetsProvider _assetsProvider;

    private async void Awake()
    {
        _assetsProvider = new();

        SkillsSpriteDataSouce skillsSpriteDataSouce = new(_assetsProvider);
        await skillsSpriteDataSouce.Load();
        await skillsSpriteDataSouce.Load();

        IInputProvider inputProvider = await GetInputProvider();
        TargetsProvider targetsProvider = new();

        PlayerFactory playerFactory = new(inputProvider, targetsProvider, _assetsProvider);

        ExperienceModel experienceModel = new();
        Player player = await playerFactory.Create(Vector3.zero, experienceModel);

        RoundSwordFactory roundSwordFactory = new(player.CharacterAttackParameters, _assetsProvider);

        SkillsFactory skillsFactory = new(player, roundSwordFactory);
        _skillsOpener.Init(skillsSpriteDataSouce, player.CharacterSkills, experienceModel, skillsFactory);

        CharacterUpgrades characterUpgrades = new();
        //characterUpgrades.Add(new EnemyDamageUpgrade(player.CharacterAttackParameters));

        //player.CharacterSkills.Add(skillsFactory.CreateSwordRoundAttack());
        //player.CharacterSkills.Add(skillsFactory.CreateSwordRoundAttack());
        //player.CharacterSkills.Add(skillsFactory.CreateSwordRoundAttack());
        //player.CharacterSkills.Add(skillsFactory.CreateSwordRoundAttack());

        //swordRoundAttackSkill.IncreaseLevel();
        //swordRoundAttackSkill.IncreaseLevel();
        //swordRoundAttackSkill.IncreaseLevel();
        //swordRoundAttackSkill.IncreaseLevel();

        DiamondLootFactory diamondLootFactory = new(player.LootHolder, _assetsProvider);
        DiamondFactory diamondFactory = new(targetsProvider, _assetsProvider, diamondLootFactory);

        WoodLootFactory woodLootFactory = new(player.LootHolder, _assetsProvider);
        WoodFactory woodFactory = new(targetsProvider, _assetsProvider, woodLootFactory);

        EnemyFactory enemyFactory = new(targetsProvider, _assetsProvider);

        _targetFollower.Set(player.transform);

        int obstaclesHealth = 3;
        int enemyHealth = 15;

        for (int i = 0; i < 10; i++)
        {
            await diamondFactory.Create(obstaclesHealth, new Vector3(3, 0, 3 * i), Quaternion.identity);
            await diamondFactory.Create(obstaclesHealth, new Vector3(-3, 0, 3 * i), Quaternion.identity);
            await woodFactory.Create(obstaclesHealth, new Vector3(4, 0, 4 * i), Quaternion.identity);
        }
        
        await enemyFactory.Create(enemyHealth, new Vector3(0, 0, 5), Quaternion.identity, player.Target, attackDistance: 3f);
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
