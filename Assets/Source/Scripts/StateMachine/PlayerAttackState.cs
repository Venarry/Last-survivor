public class PlayerAttackState : IState
{
    private ThirdPersonRotation _thirdPersonRotation;
    private PlayerAttackHandler _playerAttackHandler;

    public PlayerAttackState(ThirdPersonRotation thirdPersonRotation, PlayerAttackHandler playerAttackHandler)
    {
        _thirdPersonRotation = thirdPersonRotation;
        _playerAttackHandler = playerAttackHandler;
    }

    public void OnEnter()
    {
    }

    public void OnExit()
    {
        _thirdPersonRotation.RemoveTarget();
        _playerAttackHandler.RemoveTarget();
    }

    public void OnUpdate()
    {
        _playerAttackHandler.IncreaseLeftTime();
        _playerAttackHandler.TryAttack();
    }
}
