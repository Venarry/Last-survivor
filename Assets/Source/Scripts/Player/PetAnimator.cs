public class PetAnimator : CharacterAnimator
{
    private PetMovement _petMovement;
    private CharacterAttackHandler _characterAttackHandler;

    protected override bool IsMoving => _petMovement.IsMoving;

    protected override void OnAwake()
    {
        _petMovement = GetComponent<PetMovement>();
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
