using System;
using System.Collections.Generic;
using UnityEngine;

public class SkillWeaponCollisionHandler : MonoBehaviour
{
    private CharacterAttackParameters _characterAttackParameters;
    private Dictionary<TargetType, Action<Target>> _attackTypes;
    private float _damageMultiplier = 0.6f;

    public void Init(CharacterAttackParameters characterAttackParameters, float damageMultiplier)
    {
        _characterAttackParameters = characterAttackParameters;
        _damageMultiplier = damageMultiplier;

        _attackTypes = new()
        {
            { TargetType.Enemy, (target) => target.TakeDamage(_characterAttackParameters.EnemyDamage * _damageMultiplier) },
            { TargetType.Wood, (target) => target.TakeDamage(_characterAttackParameters.WoodDamage * _damageMultiplier) },
            { TargetType.Ore, (target) => target.TakeDamage(_characterAttackParameters.OreDamage * _damageMultiplier) },
        };
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Target target))
        {
            if(target.IsFriendly == false)
            {
                float damage = _characterAttackParameters.GetDamage(target.TargetType);
                target.TakeDamage(damage * _damageMultiplier);
                //_attackTypes[target.TargetType](target);
            }
        }
    }
}
