using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackHandler : MonoBehaviour
{
    private float _timeLeft = 0;
    private CharacterAttackParameters _characterAttackParameters;
    private CharacterBuffsModel _characterBuffsModel;
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

        float attackCooldown = GetAttackCooldown();

        if (_timeLeft >= attackCooldown)
        {
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

    private float GetAttackCooldown()
    {
        IAttackSpeedBuff[] attackSpeedBuffs = _characterBuffsModel.GetBuffs<IAttackSpeedBuff>();
        float attackCooldown = _characterAttackParameters.AttackCooldown;

        foreach (IAttackSpeedBuff buff in attackSpeedBuffs)
        {
            attackCooldown = buff.ApplyCooldown(attackCooldown);
        }

        return attackCooldown;
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
