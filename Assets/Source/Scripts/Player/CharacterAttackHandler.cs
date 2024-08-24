using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttackHandler : MonoBehaviour
{
    private float _timeLeft = 0;
    private CharacterAttackParameters _characterAttackParameters;
    private Dictionary<TargetType, Action<Target>> _playerViewAttackTypes;
    private Coroutine _activeAttack;

    public event Action<float> AttackBegin;
    public event Action<Target, float> AttackEnd;

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
        if (_activeAttack != null)
        {
            return;
        }

        _timeLeft += Time.deltaTime;
    }

    public void TryAttack(Target target)
    {
        if (target == null)
            return;

        float attackCooldown = _characterAttackParameters.AttackCooldown;

        if(_activeAttack != null)
        {
            return;
        }

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

            _activeAttack = StartCoroutine(AttackWithResetTimeLeft(target, damage));
        }
    }

    public IEnumerator AttackWithResetTimeLeft(Target target, float damage)
    {
        _playerViewAttackTypes[target.TargetType](target);
        float attackDelay = _characterAttackParameters.AttackDelay;
        AttackBegin?.Invoke(attackDelay);

        yield return new WaitForSeconds(attackDelay);

        target.TakeDamage(damage);
        _timeLeft = 0;

        AttackEnd?.Invoke(target, damage);
        _activeAttack = null;
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
