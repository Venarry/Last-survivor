using System;
using UnityEngine;

public class SplashSkill : SkillBehaviour
{
    private CharacterAttackHandler _playerAttackHandler;
    private TargetsProvider _targetsProvider;
    private readonly float _splashAngle = 90;
    private readonly float _splashDistance = 6;
    private readonly float _splashDamageMultiplierPerLevel = 0.1f;
    private readonly float _baseSplashDamageMultiplier = 0.5f;

    private float SplashDamageMultiplier => _baseSplashDamageMultiplier + _splashDamageMultiplierPerLevel * (CurrentLevel - 1);

    public SplashSkill(CharacterAttackHandler playerAttackHandler, TargetsProvider targetsProvider)
    {
        _playerAttackHandler = playerAttackHandler;
        _targetsProvider = targetsProvider;
    }

    public override SkillTickType SkillTickType => SkillTickType.AwakeTick;
    public override bool HasCooldown => false;

    public override void Apply()
    {
        _playerAttackHandler.AttackEnd += OnAttack;
    }

    public override void Disable()
    {
        _playerAttackHandler.AttackEnd -= OnAttack;
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
                currentTarget.TakeDamage(damage * SplashDamageMultiplier);
            }
        }
    }

    public override string GetUpLevelDescription()
    {
        string splashDamageText = "";

        if(CurrentLevel > 0)
        {
            decimal damagePerLevel = Math.Round((decimal)_splashDamageMultiplierPerLevel * 100, 0);
            splashDamageText = $"(+{GameParamenters.TextColorStart}{damagePerLevel}%{GameParamenters.TextColorEnd})";
        }

        return $"Splash angle {_splashAngle}\n" +
            $"Splash distance {_splashDistance}\n" +
            $"Splash damage {Math.Round((decimal)SplashDamageMultiplier * 100)}% {splashDamageText}";
    }
}