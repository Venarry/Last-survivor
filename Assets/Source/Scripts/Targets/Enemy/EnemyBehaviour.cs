using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyBehaviour : MonoBehaviour
{
    private int _damage = 1;
    private float _attackDistance;
    private float _attackCooldown = 1f;
    private float _timeLeft;
    private Target _target;
    private NavMeshAgent _agent;

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

    public bool IsReach() =>
        Vector3.Distance(transform.position, _target.Position) <= _attackDistance;

    public void TryAttack()
    {
        if (_timeLeft < _attackCooldown)
            return;

        _target.TakeDamage(_damage);
        _timeLeft = 0;
    }
}
