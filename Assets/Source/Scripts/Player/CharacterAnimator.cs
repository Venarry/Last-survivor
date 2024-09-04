using System.Collections;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    private const string AnimationNameWalk = "Crouch";
    private const string AnimationNameIdle = "FightIdle";
    private const string AnimationNameAttack = "Attack";
    private const float AnimationAttackPointPercent = 0.5f;

    [SerializeField] private AnimationClip _animationAttack;
    [SerializeField] private Animator _animator;
    [SerializeField] private CharacterAttackHandler _characterAttackHandler;
    [SerializeField] private ThirdPersonMovement _thirdPersonMovement;


    private string _currentAnimation;
    private bool _isAttacking;
    private bool _canMove;
    private float _defaultAnimationSpeed = 1f;
    private Coroutine _attackAnimationCoroutine;

    private float AnimationAttackPoint => _animationAttack.length * AnimationAttackPointPercent;
    private Vector3 MoveDirection => new(_thirdPersonMovement.Direction.x, 0, _thirdPersonMovement.Direction.z);

    private void Update()
    {
        if(MoveDirection != Vector3.zero && _isAttacking == false)
        {
            _canMove = true;
            ResetAttackAnimation();
        }

        if (_canMove == true)
        {
            SetMoveAnimation();
        }
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

    private void OnAttackBegin(Target target, float attackDelay)
    {
        ResetAttackAnimation();
        ChangeAnimation(AnimationNameAttack, canRepeat: true);

        float animationSpeed = AnimationAttackPoint / attackDelay;
        _animator.speed = animationSpeed;

        float animationDuration = _animationAttack.length / animationSpeed;
        _attackAnimationCoroutine = StartCoroutine(ApplyAttackAnimation(animationDuration));

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

    private IEnumerator ApplyAttackAnimation(float duration)
    {
        yield return new WaitForSeconds(duration);

        ResetAttackAnimation();
    }

    private void ResetAttackAnimation()
    {
        if (_attackAnimationCoroutine != null)
        {
            StopCoroutine(_attackAnimationCoroutine);
            _animator.speed = _defaultAnimationSpeed;
            _attackAnimationCoroutine = null;
            SetMoveAnimation();
        }
    }

    private void SetMoveAnimation()
    {
        if (MoveDirection == Vector3.zero)
        {
            ChangeAnimation(AnimationNameIdle, transitionDuration: 0.1f);
        }
        else
        {
            ChangeAnimation(AnimationNameWalk, transitionDuration: 0.1f);
        }
    }
}
