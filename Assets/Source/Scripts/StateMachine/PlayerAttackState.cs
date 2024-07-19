public class PlayerAttackState : IState
{
    private ThirdPersonRotation _thirdPersonRotation;
    private PlayerAttackHandler _playerAttackHandler;
    private PlayerTargetSearchHandler _playerTargetSearchHandler;

    public PlayerAttackState(
        ThirdPersonRotation thirdPersonRotation,
        PlayerAttackHandler playerAttackHandler,
        PlayerTargetSearchHandler playerTargetSearchHandler)
    {
        _thirdPersonRotation = thirdPersonRotation;
        _playerAttackHandler = playerAttackHandler;
        _playerTargetSearchHandler = playerTargetSearchHandler;
    }

    public void OnEnter()
    {
    }

    public void OnUpdate()
    {
        _playerAttackHandler.IncreaseLeftTime();
        _playerAttackHandler.TryAttack();

        _playerTargetSearchHandler.TrySearchTarget();
        _playerTargetSearchHandler.TrySetSearchState();
    }

    public void OnExit()
    {
        _thirdPersonRotation.RemoveTarget();
        _playerAttackHandler.RemoveTarget();
    }
}
