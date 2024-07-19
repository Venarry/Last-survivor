using UnityEngine;

public class PlayerAttackHandler : MonoBehaviour
{
    private float _attackCooldown = 0.5f;
    private float _timeLeft = 0;
    private int _damage = 1;
    private Target _target;

    private void Awake()
    {
        _timeLeft = _attackCooldown;
    }

    private void Update()
    {
        _timeLeft += Time.deltaTime;
    }

    public void Set(Target target)
    {
        _target = target;
    }

    public void RemoveTarget()
    {
        _target = null;
    }

    public void TryAttack()
    {
        if (_timeLeft >= _attackCooldown)
        {
            _target.TakeDamage(_damage);
            _timeLeft = 0;
        }
    }
}
