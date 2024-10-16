using System.Threading.Tasks;
using UnityEngine;

public class PlayerFactory
{
    private readonly IInputProvider _inputProviderl;
    private readonly TargetsProvider<Target> _targetsProvider;
    private readonly AssetsProvider _assetProvider;
    private readonly ItemViewFactory _itemViewFactory;
    private readonly SkillsViewFactory _skillsViewFactory;
    private readonly SpritesDataSouce _spritesDataSouce;
    private readonly Transform _mainWindowInventoryParent;
    private readonly Transform _shopInventoryParent;
    private readonly Transform _skillsParent;
    private readonly GameRestartMenu _deathMenu;
    private Player _playerPrefab;

    public PlayerFactory(
        IInputProvider inputProvider,
        TargetsProvider<Target> targetsProvider,
        AssetsProvider assetProvider,
        ItemViewFactory itemViewFactory,
        SkillsViewFactory skillsViewFactory,
        SpritesDataSouce spritesDataSouce,
        Transform mainWindowInventoryParent,
        Transform shopInventoryParent,
        Transform skillsParent,
        GameRestartMenu deathMenu)
    {
        _inputProviderl = inputProvider;
        _targetsProvider = targetsProvider;
        _assetProvider = assetProvider;
        _itemViewFactory = itemViewFactory;
        _skillsViewFactory = skillsViewFactory;
        _spritesDataSouce = spritesDataSouce;
        _mainWindowInventoryParent = mainWindowInventoryParent;
        _shopInventoryParent = shopInventoryParent;
        _skillsParent = skillsParent;
        _deathMenu = deathMenu;
    }

    public async Task<Player> Create(
        Vector3 position,
        ExperienceModel experienceModel,
        HealthModel healthModel,
        CharacterBuffsModel characterBuffsModel,
        CharacterUpgradesModel<SkillBehaviour> characterSkillsModel,
        InventoryModel inventoryModel,
        CharacterAttackParameters characterAttackParameters)
    {
        _playerPrefab = await _assetProvider.LoadGameObject<Player>(AssetsKeys.Player);

        Player player = Object.Instantiate(_playerPrefab, position, Quaternion.identity);
        _assetProvider.Clear(AssetsKeys.Player);

        player.Init(
            _inputProviderl,
            characterAttackParameters,
            _targetsProvider,
            inventoryModel,
            experienceModel,
            healthModel,
            characterBuffsModel,
            characterSkillsModel,
            _itemViewFactory,
            _skillsViewFactory,
            _spritesDataSouce,
            _mainWindowInventoryParent,
            _shopInventoryParent,
            _skillsParent,
            _deathMenu);

        return player;
    }
}
