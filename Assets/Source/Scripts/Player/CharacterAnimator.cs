using System.Collections;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    private const float AnimationAttackPointPercent = 0.5f;

    [SerializeField] private AnimationClip _animationAttack;
    [SerializeField] private Animator _animator;
    [SerializeField] private CharacterAttackHandler _characterAttackHandler;
    [SerializeField] private ThirdPersonMovement _thirdPersonMovement;

    private string _animationNameCrouch = "Crouch";
    private string _animationNameIdle = "FightIdle";
    private string _animationNameAttack = "Attack";

    private Vector3 _previousDirection = Vector3.zero;
    private string _currentAnimation;
    private bool _isAttacking;
    private bool _canMove;
    private float _defaultAnimationSpeed = 1f;
    private Coroutine _attackAnimationSpeed;

    private float AnimationAttackPoint => _animationAttack.length * AnimationAttackPointPercent;
    private Vector3 MoveDirection => new(_thirdPersonMovement.Direction.x, 0, _thirdPersonMovement.Direction.z);

    private void Update()
    {
        if(MoveDirection != Vector3.zero && _isAttacking == false)
        {
            _canMove = true;
            ResetAttackAnimationSpeed();
        }

        if (_canMove == true)
        {
            if (MoveDirection != _previousDirection)
            {
                SetMoveAnimation();
            }
        }

        _previousDirection = MoveDirection;
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
        Debug.Log("attack");
        ChangeAnimation(_animationNameAttack, canRepeat: true);
        ResetAttackAnimationSpeed();

        float animationSpeed = AnimationAttackPoint / attackDelay;
        _animator.speed = animationSpeed;

        _attackAnimationSpeed = StartCoroutine(ApplyAttackAnimationSpeed(_animationAttack.length / animationSpeed));

        _isAttacking = true;
        _canMove = false;
    }

    private void OnAttackEnd(Target target, float damage)
    {
        _isAttacking = false;
    }

    private void ChangeAnimation(string name, float transitionDuration = 0.2f, bool canRepeat = false)
    {
        if (_currentAnimation == name && canRepeat == false)
            return;

        _animator.CrossFade(name, transitionDuration);
        _currentAnimation = name;
    }

    private IEnumerator ApplyAttackAnimationSpeed(float duration)
    {
        yield return new WaitForSeconds(duration);

        ResetAttackAnimationSpeed();
    }

    private void ResetAttackAnimationSpeed()
    {
        if (_attackAnimationSpeed != null)
        {
            StopCoroutine(_attackAnimationSpeed);
            _animator.speed = _defaultAnimationSpeed;
            _attackAnimationSpeed = null;
            SetMoveAnimation();
        }
    }

    private void SetMoveAnimation()
    {
        Debug.Log("Move");
        if (MoveDirection == Vector3.zero)
        {
            ChangeAnimation(_animationNameIdle, transitionDuration: 0.1f);
        }
        else
        {
            ChangeAnimation(_animationNameCrouch, transitionDuration: 0.1f);
        }
    }
}
