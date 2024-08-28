public class PlayerAttackState : IState
{
    private readonly ThirdPersonRotation _thirdPersonRotation;
    private readonly CharacterAttackHandler _playerAttackHandler;
    private readonly CharacterTargetSearcher _targetSearcher;
    private readonly IPlayerAttackStateSwitcher _playerAttackStateSwitcher;
    private Target _target;

    public PlayerAttackState(
        ThirdPersonRotation thirdPersonRotation,
        CharacterAttackHandler playerAttackHandler,
        CharacterTargetSearcher targetSearcher,
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
        if(_targetSearcher.TryGetNearestTarget(out Target target))
        {
            if(target != _target)
            {
                Set(target);
            }

            _playerAttackHandler.TryAttack(_target);
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
