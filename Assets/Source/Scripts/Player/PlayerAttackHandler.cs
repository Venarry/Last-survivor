using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackHandler : MonoBehaviour
{
    private float _timeLeft = 0;
    private CharacterAttackParameters _characterAttackParameters;
    private Dictionary<TargetType, Action<Target>> _playerAttackTypes;

    public event Action<Target, float> AttackBegin;

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

        float damage;

        switch (target.TargetType)
        {
            case TargetType.Enemy:
                damage = _characterAttackParameters.EnemyDamage;
                break;

            case TargetType.Wood:
                damage = _characterAttackParameters.WoodDamage;
                break;

            case TargetType.Ore:
                damage = _characterAttackParameters.OreDamage;
                break;

            default:
                return;
        }

        if (_timeLeft >= _characterAttackParameters.AttackCooldown)
        {
            AttackBegin?.Invoke(target, damage);

            TryAttackWithResetTimeLeft(target, damage);
        }
    }

    public void TryAttackWithResetTimeLeft(Target target, float damage)
    {
        if (_timeLeft >= _characterAttackParameters.AttackCooldown)
        {
            _playerAttackTypes[target.TargetType](target);
            target.TakeDamage(damage);
            _timeLeft = 0;
        }
    }

    private void AttackEnemy(Target target)
    {
    }

    private void AttackWood(Target target)
    {
    }
    
    private void AttackOre(Target target)
    {
    }
}
