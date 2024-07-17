using UnityEngine;

public class PlayerFactory
{
    private readonly Player _playerPrefab = Resources.Load<Player>(ResourcesPath.Player);
    private readonly IInputProvider _inputProviderl;

    public PlayerFactory(IInputProvider inputProvider)
    {
        _inputProviderl = inputProvider;
    }

    public Player Create(Vector3 position)
    {
        Player player = Object.Instantiate(_playerPrefab, position, Quaternion.identity);
        player.Init(_inputProviderl);

        return player;
    }
}
