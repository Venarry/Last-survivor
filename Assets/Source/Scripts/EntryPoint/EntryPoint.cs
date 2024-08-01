using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private TargetFollower _targetFollower;
    [SerializeField] private SkillsOpener _skillsOpener;

    private async void Awake()
    {
        SkillsSpriteDataSouce skillsSpriteDataSouce = new();
        await skillsSpriteDataSouce.Load();

        IInputProvider inputProvider = GetInputProvider();
        TargetsProvider targetsProvider = new();

        PlayerFactory playerFactory = new(inputProvider, targetsProvider);
        await playerFactory.Load();

        ExperienceModel experienceModel = new();
        Player player = playerFactory.Create(Vector3.zero, experienceModel);

        RoundSwordFactory roundSwordFactory = new(player.CharacterAttackParameters);

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

        DiamondLootFactory diamondLootFactory = new(player.LootHolder);
        DiamondFactory diamondFactory = new(targetsProvider, diamondLootFactory);

        WoodLootFactory woodLootFactory = new(player.LootHolder);
        WoodFactory woodFactory = new(targetsProvider, woodLootFactory);

        EnemyFactory enemyFactory = new(targetsProvider);

        _targetFollower.Set(player.transform);

        int obstaclesHealth = 3;
        int enemyHealth = 15;

        for (int i = 0; i < 10; i++)
        {
            diamondFactory.Create(obstaclesHealth, new Vector3(3, 0, 3 * i), Quaternion.identity);
            diamondFactory.Create(obstaclesHealth, new Vector3(-3, 0, 3 * i), Quaternion.identity);
            woodFactory.Create(obstaclesHealth, new Vector3(4, 0, 4 * i), Quaternion.identity);
        }
        
        enemyFactory.Create(enemyHealth, new Vector3(0, 0, 5), Quaternion.identity, player.Target, attackDistance: 3f);
    }

    private IInputProvider GetInputProvider()
    {
        bool isMobile = Application.isMobilePlatform;

        if (isMobile == false)
        {
            return new KeyboardInputProvider();
        }
        else
        {
            MobileInputsProviderFactory mobileInputsProviderFactory = new();
            return mobileInputsProviderFactory.Create(_canvas.transform);
        }
    }
}
