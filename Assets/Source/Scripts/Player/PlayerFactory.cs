using System.Threading.Tasks;
using UnityEngine;

public class PlayerFactory
{
    private readonly IInputProvider _inputProviderl;
    private readonly TargetsProvider _targetsProvider;
    private readonly AssetsProvider _assetProvider;
    private Player _playerPrefab;

    public PlayerFactory(IInputProvider inputProvider, TargetsProvider targetsProvider, AssetsProvider assetProvider)
    {
        _inputProviderl = inputProvider;
        _targetsProvider = targetsProvider;
        _assetProvider = assetProvider;
    }

    public async Task<Player> Create(Vector3 position, ExperienceModel experienceModel)
    {
        _playerPrefab = await _assetProvider.LoadGameObject<Player>(AssetsKeys.Player);

        Player player = Object.Instantiate(_playerPrefab, position, Quaternion.identity);
        InventoryModel inventoryModel = new();

        _assetProvider.Clear(AssetsKeys.Player);

        int maxHealth = 30;
        HealthModel healthModel = new(maxHealth);
        CharacterAttackParameters characterAttackParameters = new();
        player.Init(_inputProviderl, characterAttackParameters, _targetsProvider, inventoryModel, experienceModel, healthModel);

        return player;
    }
}
