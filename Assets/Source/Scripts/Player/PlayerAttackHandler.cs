using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackHandler : MonoBehaviour
{
    private float _timeLeft = 0;
    private CharacterAttackParameters _characterAttackParameters;
    private Dictionary<TargetType, Action<Target>> _playerAttackTypes;

    private void Awake()
    {
        _playerAttackTypes = new()
        {
            { TargetType.Enemy, AttackEnemy },
            { TargetType.Wood, AttackWood },
            { TargetType.Ore, AttackOre },
        };
    }

    public void Init(CharacterAttackParameters characterAttackParameters)
    {
        _characterAttackParameters = characterAttackParameters;
        _timeLeft = _characterAttackParameters.AttackCooldown;
    }

    private void Update()
    {
        _timeLeft += Time.deltaTime;
    }

    public void TryAttack(Target target)
    {
        if (target == null)
            return;

        if (_timeLeft >= _characterAttackParameters.AttackCooldown)
        {
            _playerAttackTypes[target.TargetType](target);
            _timeLeft = 0;
        }
    }

    private void AttackEnemy(Target target)
    {
        target.TakeDamage(_characterAttackParameters.EnemyDamage);
    }

    private void AttackWood(Target target)
    {
        target.TakeDamage(_characterAttackParameters.WoodDamage);
    }
    
    private void AttackOre(Target target)
    {
        target.TakeDamage(_characterAttackParameters.OreDamage);
    }
}
