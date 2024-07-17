using UnityEngine;

public class PlayerFactory
{
    private readonly Player _playerPrefab = Resources.Load<Player>(ResourcesPath.Player);
    private readonly IInputProvider _inputProviderl;
    private readonly TargetsProvider _targetsProvider;

    public PlayerFactory(IInputProvider inputProvider, TargetsProvider targetsProvider)
    {
        _inputProviderl = inputProvider;
        _targetsProvider = targetsProvider;
    }

    public Player Create(Vector3 position)
    {
        Player player = Object.Instantiate(_playerPrefab, position, Quaternion.identity);
        player.Init(_inputProviderl, _targetsProvider);

        return player;
    }
}
