using UnityEngine;

public class PassiveHealSkill : SkillBehaviour
{
    private readonly HealthModel _targetHealthModel;
    private readonly float _healPercentPerSecondPerLevel = 0.0075f;
    private float _baseHealPercentPerSecond = 0.015f;

    private float HealPercentPerSecond => _baseHealPercentPerSecond + _healPercentPerSecondPerLevel * (CurrentLevel - 1);

    public PassiveHealSkill(HealthModel targetHealthModel)
    {
        _targetHealthModel = targetHealthModel;
    }

    public override SkillTickType SkillTickType => SkillTickType.EveryTick;
    public override bool HasCooldown => false;

    public override string GetUpLevelDescription()
    {
        string upgradeText = "";

        if(CurrentLevel > 0)
        {
            upgradeText = $"(+{GameParamenters.TextColorStart}{_healPercentPerSecondPerLevel * 100}{GameParamenters.TextColorEnd}%)";
        }

        return $"Health per second {HealPercentPerSecond * 100}% {upgradeText}";
    }

    public override void Apply()
    {
        _targetHealthModel.Add(_targetHealthModel.MaxValue * HealPercentPerSecond * Time.deltaTime);
    }
}
