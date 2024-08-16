using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyBehaviour : MonoBehaviour
{
    private const float AttackGapMultiply = 1.2f;
    private const float AttackDelay = 1.2f;
    private const float AttackCooldown = 1f;

    private readonly CooldownTimer _cooldownTimer = new(AttackCooldown);
    private Target _target;
    private NavMeshAgent _agent;
    private WaitForSeconds _waitAttackDelay = new(AttackDelay);
    private Coroutine _activeAttackCoroutine;
    private float _damage;
    private float _attackDistance;

    public bool IsReadyToAttack => _cooldownTimer.IsReady;
    public bool TargetIsReached => Vector3.Distance(transform.position, _target.Position) <= _attackDistance;
    public bool AttackGapIsBroken => Vector3.Distance(transform.position, _target.Position) > _attackDistance * AttackGapMultiply;

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

    public void SetDamage(float damage)
    {
        _damage = damage;
    }

    public void Follow()
    {
        _agent.SetDestination(_target.Position);
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
    }

    public void TryAttack()
    {
        if (_cooldownTimer.IsReady == false)
            return;

        if(_activeAttackCoroutine == null)
        {
            StartCoroutine(Attacking());
        }

        _cooldownTimer.Reset();
    }

    private IEnumerator Attacking()
    {
        yield return _waitAttackDelay;

        if (AttackGapIsBroken == false)
        {
            _target.TakeDamage(_damage);
        }

        _activeAttackCoroutine = null;
    }
}
