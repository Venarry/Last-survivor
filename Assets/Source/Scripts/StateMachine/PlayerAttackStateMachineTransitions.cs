public class PlayerAttackStateMachineTransitions
{
    private readonly TargetSearcher _targetSearcher;
    private readonly IPlayerAttackStateSwitcher _playerAttackStateSwitcher;

    public PlayerAttackStateMachineTransitions(
        TargetSearcher targetSearcher,
        IPlayerAttackStateSwitcher playerAttackStateSwitcher)
    {
        _targetSearcher = targetSearcher;
        _playerAttackStateSwitcher = playerAttackStateSwitcher;
    }

    public void TrySetAttackState()
    {
        if (_targetSearcher.TrySearchTarget())
        {
            _playerAttackStateSwitcher.SetAttackState();
        }
    }

    public void TrySetSearchState()
    {
        if (_targetSearcher.HasNearestTarget(out _) == false)
        {
            _playerAttackStateSwitcher.SetTargetSearchState();
        }
    }
}
