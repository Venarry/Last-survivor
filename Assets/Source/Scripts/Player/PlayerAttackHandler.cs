using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackHandler : MonoBehaviour
{
    private float _attackCooldown = 0.5f;
    private float _timeLeft = 0;
    private int _damage = 1;
    private Dictionary<TargetType, Action<Target>> _playerAttackType;

    private void Awake()
    {
        _timeLeft = _attackCooldown;

        _playerAttackType = new()
        {
            { TargetType.Enemy, AttackEnemy },
            { TargetType.Wood, AttackWood },
            { TargetType.Ore, AttackOre },
        };
    }


    private void Update()
    {
        _timeLeft += Time.deltaTime;
    }

    public void TryAttack(Target target)
    {
        if (target == null)
            return;

        if (_timeLeft >= _attackCooldown)
        {
            _playerAttackType[target.TargetType](target);
            _timeLeft = 0;
        }
    }

    private void AttackEnemy(Target target)
    {
        Debug.Log($"Attack {target.TargetType}");
        target.TakeDamage(_damage);
    }

    private void AttackWood(Target target)
    {
        Debug.Log($"Attack {target.TargetType}");
        target.TakeDamage(_damage);
    }
    
    private void AttackOre(Target target)
    {
        Debug.Log($"Attack {target.TargetType}");
        target.TakeDamage(_damage);
    }
}
