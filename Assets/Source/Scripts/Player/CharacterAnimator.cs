using System.Collections;
using UnityEngine;

public abstract class CharacterAnimator : MonoBehaviour
{
    private const string AnimationNameWalk = "Walk";
    private const string AnimationNameIdle = "Idle";
    private const string AnimationNameAttack = "Attack";
    private const float AnimationAttackPointPercent = 0.5f;

    [SerializeField] private AnimationClip _animationAttack;
    [SerializeField] private Animator _animator;
    [SerializeField] private CharacterAttackHandler _characterAttackHandler;

    private string _currentAnimation;
    private bool _isAttacking;
    private bool _canMove;
    private float _defaultAnimationSpeed = 1f;
    private Coroutine _attackAnimationCoroutine;

    private float AnimationAttackPoint => _animationAttack.length * AnimationAttackPointPercent;
    protected abstract bool IsMoving { get; }

    private void Update()
    {
        if(IsMoving == true && _isAttacking == false)
        {
            _canMove = true;
            ResetAttackAnimation();
        }

        if (_canMove == true)
        {
            SetMoveAnimation();
        }
    }

    protected void OnEnable()
    {
        _characterAttackHandler.AttackBegin += OnAttackBegin;
        _characterAttackHandler.AttackEnd += OnAttackEnd;

        OnUnityEnable();
    }

    protected void OnDisable()
    {
        _characterAttackHandler.AttackBegin -= OnAttackBegin;
        _characterAttackHandler.AttackEnd -= OnAttackEnd;

        OnUnityDisable();
    }

    protected virtual void OnUnityEnable()
    {
    }

    protected virtual void OnUnityDisable()
    {
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
        if (IsMoving == false)
        {
            ChangeAnimation(AnimationNameIdle, transitionDuration: 0.1f);
        }
        else
        {
            ChangeAnimation(AnimationNameWalk, transitionDuration: 0.1f);
        }
    }
}
