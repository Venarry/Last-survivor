public class PlayerAttackState : IState
{
    private readonly ThirdPersonRotation _thirdPersonRotation;
    private readonly PlayerAttackHandler _playerAttackHandler;
    private readonly TargetSearcher _targetSearcher;
    private readonly IPlayerAttackStateSwitcher _playerAttackStateSwitcher;
    private Target _target;

    public PlayerAttackState(
        ThirdPersonRotation thirdPersonRotation,
        PlayerAttackHandler playerAttackHandler,
        TargetSearcher targetSearcher,
        IPlayerAttackStateSwitcher playerAttackStateSwitcher)
    {
        _thirdPersonRotation = thirdPersonRotation;
        _playerAttackHandler = playerAttackHandler;
        _targetSearcher = targetSearcher;
        _playerAttackStateSwitcher = playerAttackStateSwitcher;
    }

    public void Set(Target target)
    {
        _target = target;
        _thirdPersonRotation.Set(target);
    }

    public void OnEnter()
    {
    }

    public void OnUpdate()
    {
        _playerAttackHandler.TryAttack(_target);

        if(_targetSearcher.TryGetNearestTarget(out Target target))
        {
            if(target != _target)
            {
                Set(target);
            }
        }
        else
        {
            _playerAttackStateSwitcher.SetTargetSearchState();
        }
    }

    public void OnExit()
    {
        _thirdPersonRotation.RemoveTarget();
    }
}
