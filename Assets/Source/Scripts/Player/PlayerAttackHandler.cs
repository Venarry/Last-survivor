using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackHandler : MonoBehaviour
{
    private float _timeLeft = 0;
    private CharacterAttackParameters _characterAttackParameters;
    private Dictionary<TargetType, Action<Target>> _playerViewAttackTypes;

    public event Action<Target, float> AttackBegin;
    public event Action<Target, float> Attacked;

    private void Awake()
    {
        _playerViewAttackTypes = new()
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

        float attackCooldown = _characterAttackParameters.AttackCooldown;

        if (_timeLeft >= attackCooldown)
        {
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
        
            AttackBegin?.Invoke(target, damage);
            AttackWithResetTimeLeft(target, damage);
        }
    }

    public void AttackWithResetTimeLeft(Target target, float damage)
    {
        _playerViewAttackTypes[target.TargetType](target);
        target.TakeDamage(damage);
        _timeLeft = 0;

        Attacked?.Invoke(target, damage);
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
