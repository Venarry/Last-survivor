public class PlayerAttackStateMachineTransitions
{
    private readonly PlayerTargetSearcher _playerTargetSearcher;
    private readonly IPlayerAttackStateSwitcher _playerAttackStateSwitcher;

    public PlayerAttackStateMachineTransitions(
        PlayerTargetSearcher playerTargetSearcher,
        IPlayerAttackStateSwitcher playerAttackStateSwitcher)
    {
        _playerTargetSearcher = playerTargetSearcher;
        _playerAttackStateSwitcher = playerAttackStateSwitcher;
    }

    public void TrySetAttackState()
    {
        if (_playerTargetSearcher.TrySearchTarget())
        {
            _playerAttackStateSwitcher.SetAttackState();
        }
    }

    public void TrySetSearchState()
    {
        if (_playerTargetSearcher.HasNearestTarget(out _) == false)
        {
            _playerAttackStateSwitcher.SetTargetSearchState();
        }
    }
}
