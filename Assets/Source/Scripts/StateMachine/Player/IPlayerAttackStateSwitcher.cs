public interface IPlayerAttackStateSwitcher : IStateSwitcher
{
    public void SetTargetSearchState();
    public void SetAttackState(Target target);
}
