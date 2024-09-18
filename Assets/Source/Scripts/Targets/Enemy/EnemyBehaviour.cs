using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyBehaviour : MonoBehaviour
{
    private const float AttackGapMultiply = 1.2f;
    private const float AttackDelay = 1.2f;
    private const float AttackCooldown = 0.5f;

    private readonly CooldownTimer _cooldownTimer = new(AttackCooldown);
    private readonly WaitForSeconds _waitAttackDelay = new(AttackDelay);
    private Target _target;
    private NavMeshAgent _agent;
    private Coroutine _activeAttackCoroutine;
    private float _damage;
    private float _attackDistance;

    public bool IsMoving { get; private set; }
    public bool IsReadyToAttack => _cooldownTimer.IsReady;
    public bool TargetIsReach => Vector3.Distance(transform.position, _target.Position) <= _attackDistance;
    public bool AttackIsOutRange => Vector3.Distance(transform.position, _target.Position) > _attackDistance * AttackGapMultiply;

    public Action<float> AttackBegin;
    public Action AttackEnd;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    public void Init(Target target, float attackDistance, float damage)
    {
        _target = target;
        _attackDistance = attackDistance;
        _damage = damage;
    }

    private void Update()
    {
        _cooldownTimer.Tick();
    }

    private void OnEnable()
    {
        EndAttack();
    }

    public void SetDamage(float damage)
    {
        _damage = damage;
    }

    public void Follow()
    {
        _agent.SetDestination(_target.Position);
        IsMoving = true;
    }

    public void RotateToTarget()
    {
        Vector3 lookPosition = _target.Position;
        lookPosition.y = transform.position.y;

        Quaternion lookRotation = Quaternion.LookRotation(lookPosition - transform.position);

        float lerp = 8;
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, lerp * Time.deltaTime);
    }

    public void RemoveDestination()
    {
        _agent.SetDestination(transform.position);
        IsMoving = false;
    }

    public void TryAttack()
    {
        if (_cooldownTimer.IsReady == false)
            return;

        if(_activeAttackCoroutine != null)
            return;

        _activeAttackCoroutine = StartCoroutine(Attacking());
    }

    private IEnumerator Attacking()
    {
        AttackBegin?.Invoke(AttackDelay);

        yield return _waitAttackDelay;

        if (AttackIsOutRange == false)
        {
            _target.TakeDamage(_damage);
        }

        EndAttack();
    }

    private void EndAttack()
    {
        if (_activeAttackCoroutine != null)
        {
            StopCoroutine(_activeAttackCoroutine);
            _activeAttackCoroutine = null;
            _cooldownTimer.Reset();

            AttackEnd?.Invoke();
        }
    }
}
