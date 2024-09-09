using System;
using System.Collections;
using UnityEngine;

public abstract class CharacterAnimator : MonoBehaviour
{
    private const string AnimationNameWalk = "Walk";
    private const string AnimationNameIdle = "Idle";
    private const string AnimationNameAttack = "Attack";

    [SerializeField] private AnimationClip _animationAttack;
    [SerializeField] private Animator _animator;
    [SerializeField] private  float _animationAttackPointPercent = 0.5f;

    private readonly float _defaultAnimationSpeed = 1f;
    private string _currentAnimation;
    private bool _isAttacking;
    private bool _canMove;
    private Coroutine _applyAttackAnimationCoroutine;

    private float AnimationAttackPoint => _animationAttack.length * _animationAttackPointPercent;
    protected abstract bool IsMoving { get; }

    protected void Awake()
    {
        OnAwake();
    }

    protected virtual void OnAwake()
    {
    }

    private void Update()
    {
        if (IsMoving == true && _isAttacking == false)
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
        OnUnityEnable();
    }

    protected void OnDisable()
    {
        OnUnityDisable();
    }

    protected virtual void OnUnityEnable()
    {
    }

    protected virtual void OnUnityDisable()
    {
    }

    protected void StartAttack(float attackDelay)
    {
        ResetAttackAnimation();
        ChangeAnimation(AnimationNameAttack, canRepeat: true);

        float animationSpeed = AnimationAttackPoint / attackDelay;
        _animator.speed = animationSpeed;

        float animationDuration = _animationAttack.length / animationSpeed;
        _applyAttackAnimationCoroutine = StartCoroutine(ApplyAttackAnimation(animationDuration));

        _isAttacking = true;
        _canMove = false;
    }

    protected void EndAttack()
    {
        _isAttacking = false;
    }

    private void ChangeAnimation(string name, float transitionDuration = 0.1f, bool canRepeat = false)
    {
        if (_currentAnimation == name && canRepeat == false)
            return;

        _animator.CrossFadeInFixedTime(name, transitionDuration);
        _currentAnimation = name;
    }

    private IEnumerator ApplyAttackAnimation(float duration)
    {
        yield return new WaitForSeconds(duration);

        ResetAttackAnimation();
        SetMoveAnimation();
    }

    private void ResetAttackAnimation()
    {
        if (_applyAttackAnimationCoroutine == null)
            return;

        StopCoroutine(_applyAttackAnimationCoroutine);
        _applyAttackAnimationCoroutine = null;

        _animator.speed = _defaultAnimationSpeed;
        _currentAnimation = "";
    }

    private void SetMoveAnimation()
    {
        if (IsMoving == false)
        {
            ChangeAnimation(AnimationNameIdle);
        }
        else
        {
            ChangeAnimation(AnimationNameWalk);
        }
    }
}
