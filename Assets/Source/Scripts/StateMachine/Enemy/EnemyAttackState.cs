public class EnemyAttackState : IState
{
    private readonly EnemyBehaviour _enemyBehaviour;
    private readonly IEnemyStateSwitcher _enemyStateSwitcher;

    public EnemyAttackState(EnemyBehaviour enemyBehaviour, IEnemyStateSwitcher enemyStateSwitcher)
    {
        _enemyBehaviour = enemyBehaviour;
        _enemyStateSwitcher = enemyStateSwitcher;
    }

    public void OnEnter()
    {
        
    }

    public void OnUpdate()
    {
        _enemyBehaviour.RotateToTarget();

        if (_enemyBehaviour.IsReadyToAttack == false)
            return;

        if (_enemyBehaviour.AttackGapIsBroken)
        {
            _enemyStateSwitcher.SetFollowState();
        }

        _enemyBehaviour.TryAttack();
    }

    public void OnExit()
    {
    }
}