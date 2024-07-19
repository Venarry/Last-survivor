public class PlayerTargetSearchState : IState
{
    private readonly TargetSearcher _targetSearcher;
    private readonly IPlayerAttackStateSwitcher _playerAttackStateSwitcher;

    public PlayerTargetSearchState(TargetSearcher targetSearcher, IPlayerAttackStateSwitcher playerAttackStateSwitcher)
    {
        _targetSearcher = targetSearcher;
        _playerAttackStateSwitcher = playerAttackStateSwitcher;
    }

    public void OnEnter()
    {
    }

    public void OnUpdate()
    {
        if(_targetSearcher.TryGetNearestTarget(out Target target))
        {
            _playerAttackStateSwitcher.SetAttackState(target);
        }
    }

    public void OnExit()
    {
    }
}
