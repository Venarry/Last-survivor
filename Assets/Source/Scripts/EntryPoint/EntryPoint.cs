using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private TargetFollower _targetFollower;

    private void Awake()
    {
        IInputProvider inputProvider;
        TargetsProvider targetsProvider = new();

        bool isMobile = Application.isMobilePlatform;

        if (isMobile == false)
        {
            inputProvider = new KeyboardInputProvider();
        }
        else
        {
            MobileInputsProviderFactory mobileInputsProviderFactory = new();
            inputProvider = mobileInputsProviderFactory.Create(_canvas.transform);
        }

        PlayerFactory playerFactory = new(inputProvider, targetsProvider);
        Player player = playerFactory.Create(Vector3.zero);

        DiamondLootFactory diamondLootFactory = new(player.LootHolder);
        DiamondFactory diamondFactory = new(targetsProvider, diamondLootFactory);

        WoodLootFactory woodLootFactory = new(player.LootHolder);
        WoodFactory woodFactory = new(targetsProvider, woodLootFactory);

        EnemyFactory enemyFactory = new(targetsProvider);

        _targetFollower.Set(player.transform);

        diamondFactory.Create(new Vector3(3, 0, 3), Quaternion.identity);
        diamondFactory.Create(new Vector3(-3, 0, 3), Quaternion.identity);
        woodFactory.Create(new Vector3(4, 0, 4), Quaternion.identity);
        enemyFactory.Create(new Vector3(0, 0, 5), Quaternion.identity, player.Target, attackDistance: 3f);
    }
}
