using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CharacterAttackHandler : MonoBehaviour
{
    private float _timeLeft = 0;
    private CharacterAttackParameters _characterAttackParameters;
    private CharacterBuffsModel _characterBuffsModel;
    private Coroutine _activeAttack;
    private Target _currentTarget;
    private float _attackDamageMultiplier = 1;
    private float _attackCooldownMultiplier = 1;

    public event Action<Target, float> AttackBegin;
    public event Action<Target, float> AttackEnd;

    public bool ReadyToAttack => _timeLeft >= _characterAttackParameters.AttackCooldown * _attackCooldownMultiplier;

    public void Init(CharacterAttackParameters characterAttackParameters, CharacterBuffsModel characterBuffsModel)
    {
        _characterAttackParameters = characterAttackParameters;
        _characterBuffsModel = characterBuffsModel;

        _timeLeft = _characterAttackParameters.AttackCooldown;
    }

    private void Update()
    {
        _timeLeft += Time.deltaTime;
    }

    public void SetParameters(float damageMultiplier, float cooldownMultiplier)
    {
        _attackDamageMultiplier = damageMultiplier;
        _attackCooldownMultiplier = cooldownMultiplier;
    }

    public void TryAttack(Target target)
    {
        if (target == null)
            return;

        if(_activeAttack != null)
        {
            return;
        }

        if (ReadyToAttack == false)
        {
            return;
        }

        if (_currentTarget != target)
        {
            StopAttack();

            if(_currentTarget != null)
            {
                _currentTarget.LifeCycleEnded -= OnTargetEnd;
            }

            _currentTarget = target;
            _currentTarget.LifeCycleEnded += OnTargetEnd;
        }

        float damage = _characterAttackParameters.GetDamage(target.TargetType) * _attackDamageMultiplier;
        _activeAttack = StartCoroutine(AttackWithResetTimeLeft(target, damage));
    }

    private void OnTargetEnd(Target target)
    {
        _currentTarget.LifeCycleEnded -= OnTargetEnd;
    }

    private IEnumerator AttackWithResetTimeLeft(Target target, float damage)
    {
        float attackDelay = _characterAttackParameters.AttackDelay;
        AttackBegin?.Invoke(target, attackDelay);

        yield return new WaitForSeconds(attackDelay);

        ICritDamageBuff[] critDamageBuffs = _characterBuffsModel.GetBuffs<ICritDamageBuff>();
        critDamageBuffs = critDamageBuffs.OrderBy(c => c.DamageMultiplier).ToArray();
        float buffedDamage = damage;

        foreach (ICritDamageBuff buff in critDamageBuffs)
        {
            if(buff.TryGetCrit(damage, out buffedDamage) == true)
            {
                break;
            }
        }

        target.TakeDamage(buffedDamage);
        _timeLeft = 0;

        AttackEnd?.Invoke(target, damage);
        _activeAttack = null;
    }

    private void StopAttack()
    {
        if (_activeAttack == null)
            return;

        StopCoroutine(_activeAttack);
        _activeAttack = null;

        AttackEnd?.Invoke(_currentTarget, 0);
    }
}
