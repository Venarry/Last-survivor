using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    private const float AnimationAttackPointPercent = 0.43f;

    [SerializeField] private AnimationClip _animationAttack;
    [SerializeField] private Animator _animator;
    [SerializeField] private CharacterAttackHandler _characterAttackHandler;
    [SerializeField] private ThirdPersonMovement _thirdPersonMovement;

    private int _animationWalkHash = Animator.StringToHash("Crouch");
    private int _animationAttackHash = Animator.StringToHash("Attack");
    private Vector3 _previousDirection = Vector3.zero;

    private float AnimationAttackPoint => _animationAttack.length * AnimationAttackPointPercent;

    private void Update()
    {
        Vector3 currentDirection = new(_thirdPersonMovement.Direction.x, 0, _thirdPersonMovement.Direction.z);

        if(_thirdPersonMovement.Direction != _previousDirection)
        {
            if (currentDirection == Vector3.zero)
            {
                _animator.SetBool(_animationWalkHash, false);
            }
            else
            {
                _animator.SetBool(_animationWalkHash, true);
            }
        }

        _previousDirection = _thirdPersonMovement.Direction;
    }

    private void OnEnable()
    {
        _characterAttackHandler.AttackBegin += OnAttackBegin;
        _characterAttackHandler.AttackEnd += OnAttackEnd;
    }

    private void OnDisable()
    {
        _characterAttackHandler.AttackBegin -= OnAttackBegin;
        _characterAttackHandler.AttackEnd -= OnAttackEnd;
    }

    private void OnAttackBegin(float attackDelay)
    {
        _animator.SetBool(_animationAttackHash, true);
        _animator.speed = AnimationAttackPoint / attackDelay;
    }

    private void OnAttackEnd(Target target, float damage)
    {
        _animator.SetBool(_animationAttackHash, false);
        _animator.speed = 1f;
    }
}
