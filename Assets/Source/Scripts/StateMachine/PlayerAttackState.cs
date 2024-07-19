public class PlayerAttackState : IState
{
    private readonly ThirdPersonRotation _thirdPersonRotation;
    private readonly PlayerAttackHandler _playerAttackHandler;
    private readonly TargetSearcher _targetSearcher;
    private readonly PlayerAttackStateMachineTransitions _playerAttackStateMachineTransitions;

    public PlayerAttackState(
        ThirdPersonRotation thirdPersonRotation,
        PlayerAttackHandler playerAttackHandler,
        TargetSearcher targetSearcher,
        PlayerAttackStateMachineTransitions playerAttackStateMachineTransitions)
    {
        _thirdPersonRotation = thirdPersonRotation;
        _playerAttackHandler = playerAttackHandler;
        _targetSearcher = targetSearcher;
        _playerAttackStateMachineTransitions = playerAttackStateMachineTransitions;
    }

    public void OnEnter()
    {
    }

    public void OnUpdate()
    {
        _playerAttackHandler.TryAttack();

        _targetSearcher.TrySearchTarget();
        _playerAttackStateMachineTransitions.TrySetSearchState();
    }

    public void OnExit()
    {
        _thirdPersonRotation.RemoveTarget();
        _playerAttackHandler.RemoveTarget();
    }
}
