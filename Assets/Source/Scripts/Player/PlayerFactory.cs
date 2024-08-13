using System.Threading.Tasks;
using UnityEngine;

public class PlayerFactory
{
    private readonly IInputProvider _inputProviderl;
    private readonly TargetsProvider _targetsProvider;
    private readonly AssetsProvider _assetProvider;
    private ItemViewFactory _itemViewFactory;
    private SpritesDataSouce _spritesDataSouce;
    private Transform _itemsParent;
    private Player _playerPrefab;

    public PlayerFactory(
        IInputProvider inputProvider,
        TargetsProvider targetsProvider,
        AssetsProvider assetProvider,
        ItemViewFactory itemViewFactory,
        SpritesDataSouce spritesDataSouce,
        Transform itemsParent)
    {
        _inputProviderl = inputProvider;
        _targetsProvider = targetsProvider;
        _assetProvider = assetProvider;
        _itemViewFactory = itemViewFactory;
        _spritesDataSouce = spritesDataSouce;
        _itemsParent = itemsParent;
    }

    public async Task<Player> Create(Vector3 position, ExperienceModel experienceModel, HealthModel healthModel)
    {
        _playerPrefab = await _assetProvider.LoadGameObject<Player>(AssetsKeys.Player);

        Player player = Object.Instantiate(_playerPrefab, position, Quaternion.identity);
        InventoryModel inventoryModel = new();

        _assetProvider.Clear(AssetsKeys.Player);

        CharacterAttackParameters characterAttackParameters = new();
        player.Init(
            _inputProviderl,
            characterAttackParameters,
            _targetsProvider, inventoryModel,
            experienceModel, healthModel,
            _itemViewFactory,
            _spritesDataSouce,
            _itemsParent);

        return player;
    }
}
