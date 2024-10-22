using System;
using UnityEngine;
using YG;

public class PassiveHealSkill : SkillBehaviour
{
    private readonly HealthModel _targetHealthModel;
    private readonly float _healPercentPerSecondPerLevel = 0.0075f;
    private readonly float _baseHealPercentPerSecond = 0.015f;

    private float HealPercentPerSecond => _baseHealPercentPerSecond + _healPercentPerSecondPerLevel * (CurrentLevel - 1);

    public PassiveHealSkill(HealthModel targetHealthModel)
    {
        _targetHealthModel = targetHealthModel;
    }

    public override UpgradeType UpgradeType => UpgradeType.PassiveHealthRegen;
    public override SkillTickType SkillTickType => SkillTickType.EveryTick;
    public override bool HasCooldown => false;

    public override void Apply()
    {
        _targetHealthModel.Add(_targetHealthModel.MaxValue * HealPercentPerSecond * Time.deltaTime);
    }

    public override string GetUpLevelDescription()
    {
        string upgradeText = "";

        if(CurrentLevel > 0)
        {
            upgradeText = $"(+{GameParameters.TextColorStart}{_healPercentPerSecondPerLevel * 100}{GameParameters.TextColorEnd}%)";
        }

        string healthPerSecondText;

        switch (YandexGame.lang)
        {
            case GameParameters.CodeRu:
                healthPerSecondText = "Здоровье в секунду";
                break;

            case GameParameters.CodeTr:
                healthPerSecondText = "Saniyede sağlık";
                break;

            default:
                healthPerSecondText = "Health per second";
                break;
        }

        return $"{healthPerSecondText} {HealPercentPerSecond * 100}% {upgradeText}";
    }
}
