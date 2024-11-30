namespace StateMachine
{
    public interface IStateSwitcher
    {
        public void Switch<T>()
            where T : IState;
    }
}