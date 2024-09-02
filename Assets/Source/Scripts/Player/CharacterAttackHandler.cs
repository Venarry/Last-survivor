using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterAttackHandler : MonoBehaviour
{
    private float _timeLeft = 0;
    private CharacterAttackParameters _characterAttackParameters;
    private CharacterBuffsModel _characterBuffsModel;
    private Coroutine _activeAttack;

    public event Action<Target, float> AttackBegin;
    public event Action<Target, float> AttackEnd;

    public bool ReadyToAttack => _timeLeft >= _characterAttackParameters.AttackCooldown;

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

    public void TryAttack(Target target)
    {
        if (target == null)
            return;

        if(_activeAttack != null)
        {
            return;
        }

        if (ReadyToAttack)
        {
            float damage = _characterAttackParameters.GetDamage(target.TargetType);

            _activeAttack = StartCoroutine(AttackWithResetTimeLeft(target, damage));
        }
    }

    public IEnumerator AttackWithResetTimeLeft(Target target, float damage)
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
}
