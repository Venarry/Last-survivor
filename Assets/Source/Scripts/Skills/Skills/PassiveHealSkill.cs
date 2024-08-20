using UnityEngine;

public class PassiveHealSkill : SkillBehaviour
{
    private readonly HealthModel _targetHealthModel;
    private float _healPercentPerSecond = 0.01f;
    private float _healPercentPerSecondPerLevel = 0.01f;

    public PassiveHealSkill(HealthModel targetHealthModel)
    {
        _targetHealthModel = targetHealthModel;
    }

    public override SkillTickType SkillTickType => SkillTickType.EveryTick;
    public override bool HasCooldown => false;

    public override string GetUpgradeDescription()
    {
        string upgradeText = "";

        if(CurrentLevel > 0)
        {
            upgradeText = $"(+{GameParamenters.TextColorStart}{_healPercentPerSecondPerLevel * 100}{GameParamenters.TextColorEnd}%)";
        }

        return $"Health per second {_healPercentPerSecond * 100}% {upgradeText}";
    }

    public override void TryCast()
    {
        _targetHealthModel.Add(_targetHealthModel.MaxValue * _healPercentPerSecond * Time.deltaTime);
    }

    protected override void OnLevelAdd()
    {
        _healPercentPerSecond += _healPercentPerSecondPerLevel;
    }
}
