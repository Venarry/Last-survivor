public class PlayerAttackStateMachine : StateMachine, IPlayerAttackStateSwitcher
{
    public void SetTargetSearchState()
    {
        Switch<PlayerTargetSearchState>();
    }

    public void SetAttackState()
    {
        Switch<PlayerAttackState>();
    }
}
