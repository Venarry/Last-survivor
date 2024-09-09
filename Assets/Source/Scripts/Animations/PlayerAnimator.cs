public class PlayerAnimator : CharacterAnimator
{
    private IMoveProvider _moveProvider;
    private CharacterAttackHandler _characterAttackHandler;

    protected override bool IsMoving => _moveProvider.IsMoving;

    protected override void OnAwake()
    {
        _moveProvider = GetComponent<IMoveProvider>();
        _characterAttackHandler = GetComponent<CharacterAttackHandler>();
    }

    protected override void OnUnityEnable()
    {
        _characterAttackHandler.AttackBegin += OnAttackBegin;
        _characterAttackHandler.AttackEnd += OnAttackEnd;
    }

    protected override void OnUnityDisable()
    {
        _characterAttackHandler.AttackBegin -= OnAttackBegin;
        _characterAttackHandler.AttackEnd -= OnAttackEnd;
    }

    private void OnAttackBegin(Target target, float delay) =>
        StartAttack(delay);

    private void OnAttackEnd(Target target, float damage) => 
        EndAttack();
}
