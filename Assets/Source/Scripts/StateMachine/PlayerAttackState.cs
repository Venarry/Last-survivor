public class PlayerAttackState : IState
{
    private readonly ThirdPersonRotation _thirdPersonRotation;
    private readonly PlayerAttackHandler _playerAttackHandler;
    private readonly PlayerTargetSearcher _playerTargetSearcher;
    private readonly PlayerAttackStateMachineTransitions _playerAttackStateMachineTransitions;

    public PlayerAttackState(
        ThirdPersonRotation thirdPersonRotation,
        PlayerAttackHandler playerAttackHandler,
        PlayerTargetSearcher playerTargetSearchHandler,
        PlayerAttackStateMachineTransitions playerAttackStateMachineTransitions)
    {
        _thirdPersonRotation = thirdPersonRotation;
        _playerAttackHandler = playerAttackHandler;
        _playerTargetSearcher = playerTargetSearchHandler;
        _playerAttackStateMachineTransitions = playerAttackStateMachineTransitions;
    }

    public void OnEnter()
    {
    }

    public void OnUpdate()
    {
        _playerAttackHandler.TryAttack();

        _playerTargetSearcher.TrySearchTarget();
        _playerAttackStateMachineTransitions.TrySetSearchState();
    }

    public void OnExit()
    {
        _thirdPersonRotation.RemoveTarget();
        _playerAttackHandler.RemoveTarget();
    }
}
