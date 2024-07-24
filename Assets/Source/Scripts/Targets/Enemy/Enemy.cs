using UnityEngine;

[RequireComponent(typeof(EnemyStateMachine))]
[RequireComponent(typeof(EnemyBehaviour))]
public class Enemy : MonoBehaviour
{
    private EnemyStateMachine _enemyStateMachine;
    private EnemyBehaviour _enemyBehaviour;

    private void Awake()
    {
        _enemyStateMachine = GetComponent<EnemyStateMachine>();
        _enemyBehaviour = GetComponent<EnemyBehaviour>();
    }

    public void Init(Target attackTarget, float attackDistance)
    {
        _enemyStateMachine.Init();
        _enemyBehaviour.Init(attackTarget, attackDistance);
    }
}
