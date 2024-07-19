public class PlayerAttackStateMachineTransitions
{
    private readonly TargetSearcher _targetSearcher;
    private readonly IPlayerAttackStateSwitcher _playerAttackStateSwitcher;

    public PlayerAttackStateMachineTransitions(
        TargetSearcher targetSearcher,
        IPlayerAttackStateSwitcher playerAttackStateSwitcher)
    {
        _targetSearcher = targetSearcher;
        _playerAttackStateSwitcher = playerAttackStateSwitcher;
    }
}
