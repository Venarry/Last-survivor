using System;
using UnityEngine;

public class SplashSkill : SkillBehaviour
{
    private PlayerAttackHandler _playerAttackHandler;
    private TargetsProvider _targetsProvider;
    private float _splashAngle = 90;
    private float _splashDistance = 6;
    private float _splashDamageMultiplier = 0.4f;
    private float _splashDamageMultiplierForLevel = 0.1f;

    public SplashSkill(PlayerAttackHandler playerAttackHandler, TargetsProvider targetsProvider)
    {
        _playerAttackHandler = playerAttackHandler;
        _targetsProvider = targetsProvider;
    }

    public override SkillTickType SkillTickType => SkillTickType.AwakeTick;
    public override bool HasCooldown => false;

    public override void Apply()
    {
        _playerAttackHandler.Attacked += OnAttack;
    }

    public override void Disable()
    {
        _playerAttackHandler.Attacked -= OnAttack;
    }

    protected override void OnLevelAdd()
    {
        _splashDamageMultiplier += _splashDamageMultiplierForLevel;
    }

    private void OnAttack(Target target, float damage)
    {
        Vector3 playerPosition = _playerAttackHandler.transform.position;
        Target[] targets = _targetsProvider.Targets;

        foreach (Target currentTarget in targets)
        {
            if(target == currentTarget)
            {
                continue;
            }

            if (Vector3.Distance(playerPosition, currentTarget.Position) > _splashDistance)
            {
                continue;
            }

            Vector3 targetDirection = (currentTarget.Position - playerPosition).normalized;

            if (Vector3.Angle(_playerAttackHandler.transform.forward, targetDirection) <= _splashAngle / 2)
            {
                currentTarget.TakeDamage(damage * _splashDamageMultiplier);
            }
        }
    }

    public override string GetUpgradeDescription()
    {
        string splashDamageText = "";

        if(CurrentLevel > 0)
        {
            decimal danage = Math.Round((decimal)_splashDamageMultiplierForLevel * 100, 0);
            splashDamageText = $"(+{GameParamenters.TextColorStart}{danage}%{GameParamenters.TextColorEnd})";
        }

        return $"Splash angle {_splashAngle}\n" +
        $"Splash distance {_splashDistance}\n" +
        $"Splash damage {_splashDamageMultiplier * 100}% {splashDamageText}";
    }
}

/*public class AttackSpeedSkill : SkillBehaviour
{
    private float _attackSpeedPerLevel = 0.1f;
    public override SkillTickType SkillTickType => throw new NotImplementedException();
    public override bool HasCooldown => throw new NotImplementedException();

    protected override void OnLevelAdd()
    {
    }

    public override string GetUpgradeDescription()
    {
    }
}*/
