using UnityEngine;

public class PassiveHealSkill : SkillBehaviour
{
    private readonly HealthModel _targetHealthModel;
    private readonly float _healPercentPerSecondPerLevel = 0.0075f;
    private readonly float _baseHealPercentPerSecond = 0.015f;

    private float HealPercentPerSecond => _baseHealPercentPerSecond + _healPercentPerSecondPerLevel * (CurrentLevel - 1);

    public PassiveHealSkill(HealthModel targetHealthModel, LanguageProvider languageProvider) : base(languageProvider)
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

    public override void Disable()
    {
    }

    public override string GetUpLevelDescription()
    {
        string upgradeText = "";

        if(CurrentLevel > 0)
        {
            upgradeText = $"(+{GameParameters.TextColorStart}{_healPercentPerSecondPerLevel * 100}{GameParameters.TextColorEnd}%)";
        }

        return $"{LanguageProvider.HealthPerSecond} {HealPercentPerSecond * 100}% {upgradeText}";
    }
}
