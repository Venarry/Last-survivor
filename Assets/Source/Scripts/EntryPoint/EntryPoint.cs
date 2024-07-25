using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private TargetFollower _targetFollower;

    private void Awake()
    {
        IInputProvider inputProvider = GetInputProvider();
        TargetsProvider targetsProvider = new();

        PlayerFactory playerFactory = new(inputProvider, targetsProvider);
        Player player = playerFactory.Create(Vector3.zero);

        CharacterUpgrades characterUpgrades = new();
        characterUpgrades.Add(new EnemyDamageUpgrade(player.CharacterAttackParameters));

        RoundSwordFactory roundSwordFactory = new(player.CharacterAttackParameters);
        SwordRoundAttackSkill swordRoundAttackSkill = new(roundSwordFactory, player.transform, player.TargetSearcher);
        player.CharacterSkills.Add(swordRoundAttackSkill);

        DiamondLootFactory diamondLootFactory = new(player.LootHolder);
        DiamondFactory diamondFactory = new(targetsProvider, diamondLootFactory);

        WoodLootFactory woodLootFactory = new(player.LootHolder);
        WoodFactory woodFactory = new(targetsProvider, woodLootFactory);

        EnemyFactory enemyFactory = new(targetsProvider);

        _targetFollower.Set(player.transform);

        int obstaclesHealth = 3;
        int enemyHealth = 15;
        diamondFactory.Create(obstaclesHealth, new Vector3(3, 0, 3), Quaternion.identity);
        diamondFactory.Create(obstaclesHealth, new Vector3(-3, 0, 3), Quaternion.identity);
        woodFactory.Create(obstaclesHealth, new Vector3(4, 0, 4), Quaternion.identity);
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
