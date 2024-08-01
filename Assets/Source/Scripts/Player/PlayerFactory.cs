using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class PlayerFactory
{
    private Player _playerPrefab;
    private readonly IInputProvider _inputProviderl;
    private readonly TargetsProvider _targetsProvider;

    public PlayerFactory(IInputProvider inputProvider, TargetsProvider targetsProvider)
    {
        _inputProviderl = inputProvider;
        _targetsProvider = targetsProvider;
    }

    public async Task Load()
    {
        GameObject gameObject = await Addressables.LoadAssetAsync<GameObject>(ResourcesPath.Player).Task;
        _playerPrefab = gameObject.GetComponent<Player>();
    }

    public Player Create(Vector3 position, ExperienceModel experienceModel)
    {
        Player player = Object.Instantiate(_playerPrefab, position, Quaternion.identity);
        InventoryModel inventoryModel = new();

        int maxHealth = 30;
        HealthModel healthModel = new(maxHealth);
        CharacterAttackParameters characterAttackParameters = new();
        player.Init(_inputProviderl, characterAttackParameters, _targetsProvider, inventoryModel, experienceModel, healthModel);

        return player;
    }
}
