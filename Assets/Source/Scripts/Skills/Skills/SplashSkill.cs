using System;
using UnityEngine;
using YG;

public class SplashSkill : SkillBehaviour
{
    private readonly CharacterAttackHandler _playerAttackHandler;
    private readonly TargetsProvider<Target> _targetsProvider;
    private readonly float _splashAngle = 90;
    private readonly float _splashDistance = 6;
    private readonly float _splashDamageMultiplierPerLevel = 0.1f;
    private readonly float _baseSplashDamageMultiplier = 0.5f;

    private float SplashDamageMultiplier => _baseSplashDamageMultiplier + _splashDamageMultiplierPerLevel * Mathf.Max(CurrentLevel - 1, 0);

    public SplashSkill(CharacterAttackHandler playerAttackHandler, TargetsProvider<Target> targetsProvider)
    {
        _playerAttackHandler = playerAttackHandler;
        _targetsProvider = targetsProvider;
    }

    public override UpgradeType UpgradeType => UpgradeType.Splash;
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
        string splashAdditionalDamageText = "";

        if(CurrentLevel > 0)
        {
            decimal damagePerLevel = Math.Round((decimal)_splashDamageMultiplierPerLevel * 100, 0);
            splashAdditionalDamageText = $"(+{GameParameters.TextColorStart}{damagePerLevel}%{GameParameters.TextColorEnd})";
        }

        string splashAngleText;
        string splashDistanceText;
        string splashDamageText;

        switch (YandexGame.lang)
        {
            case GameParameters.CodeRu:
                splashAngleText = "Угол сплеша";
                splashDistanceText = "Дистанция сплеша";
                splashDamageText = "Урон от сплеша";
                break;

            case GameParameters.CodeEn:
                splashAngleText = "Splash angle";
                splashDistanceText = "Splash distance";
                splashDamageText = "Splash damage";
                break;

            case GameParameters.CodeTr:
                splashAngleText = "Sıçrama açısı";
                splashDistanceText = "Sıçrama mesafesi";
                splashDamageText = "Sıçrama hasarı";
                break;

            default:
                throw new ArgumentNullException(nameof(YandexGame.lang));
        }

        return $"{splashAngleText} {_splashAngle}\n" +
            $"{splashDistanceText} {_splashDistance}\n" +
            $"{splashDamageText} {Math.Round((decimal)SplashDamageMultiplier * 100)}% {splashAdditionalDamageText}";
    }
}