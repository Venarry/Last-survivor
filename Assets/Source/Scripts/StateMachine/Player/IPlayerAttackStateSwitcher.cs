using Targets;

namespace StateMachine.Player
{
    public interface IPlayerAttackStateSwitcher : IStateSwitcher
    {
        public void SetTargetSearchState();
        public void SetAttackState(Target target);
    }
}