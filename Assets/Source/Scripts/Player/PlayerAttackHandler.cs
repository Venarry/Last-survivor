using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackHandler : MonoBehaviour
{
    private float _attackCooldown = 0.5f;
    private float _timeLeft = 0;
    private int _damage = 1;
    private Target _target;
    private Dictionary<TargetType, Action> _playerAttackType = new()
    {
        //{ TargetType.Ore,  }
    };

    private void Awake()
    {
        _timeLeft = _attackCooldown;
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
            target.TakeDamage(_damage);
            _timeLeft = 0;
        }
    }

    private void AttackOre()
    {

    }
}
