using System;
using System.Collections.Generic;
using UnityEngine;

public class RoundSwordCollisionHandler : MonoBehaviour
{
    private CharacterAttackParameters _characterAttackParameters;
    private Dictionary<TargetType, Action<Target>> _attackTypes;

    public void Init(CharacterAttackParameters characterAttackParameters)
    {
        _characterAttackParameters = characterAttackParameters;

        _attackTypes = new()
        {
            { TargetType.Enemy, (target) => target.TakeDamage(_characterAttackParameters.EnemyDamage) },
            { TargetType.Wood, (target) => target.TakeDamage(_characterAttackParameters.WoodDamage) },
            { TargetType.Ore, (target) => target.TakeDamage(_characterAttackParameters.OreDamage) },
        };
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Target target))
        {
            if(target.IsFriendly == false)
            {
                _attackTypes[target.TargetType](target);
            }
        }
    }
}
