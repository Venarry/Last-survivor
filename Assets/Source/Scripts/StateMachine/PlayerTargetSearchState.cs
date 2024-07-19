public class PlayerTargetSearchState : IState
{
    private readonly PlayerAttackStateMachineTransitions _playerAttackStateMachineTransitions;

    public PlayerTargetSearchState(
        PlayerAttackStateMachineTransitions playerAttackStateMachineTransitions)
    {
        _playerAttackStateMachineTransitions = playerAttackStateMachineTransitions;
    }

    public void OnEnter()
    {
    }

    public void OnUpdate()
    {
        _playerAttackStateMachineTransitions.TrySetAttackState();
    }

    public void OnExit()
    {
    }
}
