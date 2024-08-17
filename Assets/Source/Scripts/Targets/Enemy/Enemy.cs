using UnityEngine;

[RequireComponent(typeof(EnemyStateMachine))]
[RequireComponent(typeof(EnemyBehaviour))]
public class Enemy : Target
{
    private EnemyStateMachine _enemyStateMachine;
    private EnemyBehaviour _enemyBehaviour;

    protected override void OnAwake()
    {
        _enemyStateMachine = GetComponent<EnemyStateMachine>();
        _enemyBehaviour = GetComponent<EnemyBehaviour>();
    }

    public void InitEnemy(Target attackTarget, float attackDistance, float damage)
    {
        _enemyStateMachine.Init();
        _enemyBehaviour.Init(attackTarget, attackDistance, damage);
    }

    public void ResetDamage(float damage)
    {
        _enemyBehaviour.SetDamage(damage);
    }
}
