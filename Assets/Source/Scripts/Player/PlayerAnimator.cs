using UnityEngine;

public class PlayerAnimator : CharacterAnimator
{
    private ThirdPersonMovement _thirdPersonMovement;
    private CharacterAttackHandler _characterAttackHandler;

    protected override bool IsMoving => new Vector3(_thirdPersonMovement.Direction.x, 0, _thirdPersonMovement.Direction.z) != Vector3.zero;

    protected override void OnAwake()
    {
        _thirdPersonMovement = GetComponent<ThirdPersonMovement>();
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
