using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyBehaviour : MonoBehaviour
{
    private const float AttackGapMultiply = 1.2f;
    private const float AttackDelay = 1.2f;

    private int _damage = 1;
    private float _attackDistance;
    private float _attackCooldown = 1f;
    private float _timeLeft;
    private Target _target;
    private NavMeshAgent _agent;
    private WaitForSeconds _waitAttackDelay = new(AttackDelay);
    private Coroutine _activeAttackCoroutine;

    public bool IsReadyToAttack => _timeLeft >= _attackCooldown;
    public bool TargetIsReached => Vector3.Distance(transform.position, _target.Position) <= _attackDistance;
    public bool AttackGapIsBroken => Vector3.Distance(transform.position, _target.Position) > _attackDistance * AttackGapMultiply;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    public void Init(Target target, float attackDistance)
    {
        _target = target;
        _attackDistance = attackDistance;
    }

    private void Update()
    {
        _timeLeft += Time.deltaTime;
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
        _agent.ResetPath();
    }

    public void TryAttack()
    {
        if (IsReadyToAttack == false)
            return;

        if(_activeAttackCoroutine == null)
            _activeAttackCoroutine = StartCoroutine(Attacking());

        _timeLeft = 0;
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
