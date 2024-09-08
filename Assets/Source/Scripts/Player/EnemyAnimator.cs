public class EnemyAnimator : CharacterAnimator
{
    private EnemyBehaviour _enemyBehaviour;
    protected override bool IsMoving => _enemyBehaviour.IsMoving;

    protected override void OnAwake()
    {
        _enemyBehaviour = GetComponent<EnemyBehaviour>();
    }

    protected override void OnUnityEnable()
    {
        _enemyBehaviour.AttackBegin += OnAttackBegin;
        _enemyBehaviour.AttackEnd += OnAttackEnd;
    }

    protected override void OnUnityDisable()
    {
        _enemyBehaviour.AttackBegin -= OnAttackBegin;
        _enemyBehaviour.AttackEnd -= OnAttackEnd;
    }

    private void OnAttackBegin(float delay)
    {
        StartAttack(delay);
    }
    private void OnAttackEnd()
    {
        EndAttack();
    }
}
