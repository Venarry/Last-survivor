public class EnemyStateMachine : StateMachine, IEnemyStateSwitcher
{
    private EnemyBehaviour _enemyBehaviour;
    private IEnemyStateSwitcher _enemyStateSwitcher;

    private EnemyFollowState _enemyFollowState;
    private EnemyAttackState _enemyAttackState;

    public void Init()
    {
        _enemyBehaviour = GetComponent<EnemyBehaviour>();
        _enemyStateSwitcher = GetComponent<IEnemyStateSwitcher>();

        _enemyFollowState = new(_enemyBehaviour, _enemyStateSwitcher);
        Register(_enemyFollowState);

        _enemyAttackState = new(_enemyBehaviour, _enemyStateSwitcher);
        Register(_enemyAttackState);

        SetFollowState();
    }

    public void SetFollowState()
    {
        Switch<EnemyFollowState>();
    }

    public void SetAttackState() 
    {
        Switch<EnemyAttackState>();
    }
}