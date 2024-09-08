using System;

public class EnemyAttackState : IState
{
    private readonly EnemyBehaviour _enemyBehaviour;
    private readonly IEnemyStateSwitcher _enemyStateSwitcher;

    public EnemyAttackState(EnemyBehaviour enemyBehaviour, IEnemyStateSwitcher enemyStateSwitcher)
    {
        _enemyBehaviour = enemyBehaviour;
        _enemyStateSwitcher = enemyStateSwitcher;

        _enemyBehaviour.AttackEnd += OnAttackEnd;
    }

    ~EnemyAttackState() 
    {
        _enemyBehaviour.AttackEnd -= OnAttackEnd;
    }

    public void OnEnter()
    {
    }

    public void OnUpdate()
    {
        _enemyBehaviour.RotateToTarget();

        if (_enemyBehaviour.IsReadyToAttack == false)
            return;

        _enemyBehaviour.TryAttack();
    }

    public void OnExit()
    {
    }

    private void OnAttackEnd()
    {
        if (_enemyBehaviour.AttackIsOutRange)
        {
            _enemyStateSwitcher.SetFollowState();
        }
    }
}