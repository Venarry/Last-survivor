public class EnemyFollowState : IState
{
    private readonly EnemyBehaviour _enemyBehaviour;
    private readonly IEnemyStateSwitcher _enemyStateSwitcher;

    public EnemyFollowState(EnemyBehaviour enemyBehaviour, IEnemyStateSwitcher enemyStateSwitcher)
    {
        _enemyBehaviour = enemyBehaviour;
        _enemyStateSwitcher = enemyStateSwitcher;
    }

    public void OnEnter()
    {
    }

    public void OnUpdate()
    {
        _enemyBehaviour.Follow();

        if (_enemyBehaviour.IsReach())
        {
            _enemyBehaviour.RemoveDestination();
            _enemyStateSwitcher.SetAttackState();
        }
    }

    public void OnExit()
    {
    }
}
