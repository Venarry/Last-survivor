public class PlayerAttackStateMachine : StateMachine, IPlayerAttackStateSwitcher
{
    private PlayerTargetSearchState _playerTargetSearchState;
    private PlayerAttackState _playerAttackState;

    public void Init(
        CharacterTargetSearcher targetSearcher,
        ThirdPersonRotation thirdPersonRotation,
        CharacterAttackHandler playerAttackHandler,
        IPlayerAttackStateSwitcher playerAttackStateSwitcher)
    {
        _playerTargetSearchState = new(targetSearcher, playerAttackStateSwitcher);
        _playerAttackState = new(
            thirdPersonRotation, playerAttackHandler, targetSearcher, playerAttackStateSwitcher);

        Register(_playerTargetSearchState);
        Register(_playerAttackState);

        SetTargetSearchState();
    }

    public void SetTargetSearchState()
    {
        Switch<PlayerTargetSearchState>();
    }

    public void SetAttackState(Target target)
    {
        _playerAttackState.Set(target);
        Switch<PlayerAttackState>();
    }
}
