using System.Threading.Tasks;
using UnityEngine;

public class PlayerFactory
{
    private readonly IInputProvider _inputProviderl;
    private readonly TargetsProvider _targetsProvider;
    private readonly AssetProvider _assetProvider;
    private Player _playerPrefab;

    public PlayerFactory(IInputProvider inputProvider, TargetsProvider targetsProvider, AssetProvider assetProvider)
    {
        _inputProviderl = inputProvider;
        _targetsProvider = targetsProvider;
        _assetProvider = assetProvider;
    }

    public async Task<Player> Create(Vector3 position, ExperienceModel experienceModel)
    {
        await Load();

        Player player = Object.Instantiate(_playerPrefab, position, Quaternion.identity);
        InventoryModel inventoryModel = new();

        _assetProvider.Clear(ResourcesPath.Player);

        int maxHealth = 30;
        HealthModel healthModel = new(maxHealth);
        CharacterAttackParameters characterAttackParameters = new();
        player.Init(_inputProviderl, characterAttackParameters, _targetsProvider, inventoryModel, experienceModel, healthModel);

        return player;
    }

    private async Task Load()
    {
        GameObject gameObject = await _assetProvider.Load<GameObject>(ResourcesPath.Player);
        _playerPrefab = gameObject.GetComponent<Player>();
    }
}
