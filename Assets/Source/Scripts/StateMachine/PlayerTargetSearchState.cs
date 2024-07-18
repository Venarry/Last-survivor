public class PlayerTargetSearchState : IState
{
    private readonly ThirdPersonRotation _thirdPersonRotation;
    private readonly PlayerTargetSearchHandler _targetSearchHandler;

    public PlayerTargetSearchState(
        ThirdPersonRotation thirdPersonRotation,
        PlayerTargetSearchHandler targetSearchHandler)
    {
        _thirdPersonRotation = thirdPersonRotation;
        _targetSearchHandler = targetSearchHandler;
    }

    public void OnEnter()
    {
        _thirdPersonRotation.RemoveTarget();
    }

    public void OnUpdate()
    {
        _targetSearchHandler.SearchEnemy();
    }

    public void OnExit()
    {
    }
}
