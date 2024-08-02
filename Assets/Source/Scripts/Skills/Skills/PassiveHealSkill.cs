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

    public override void TryCast()
    {
        _targetHealthModel.Add(_targetHealthModel.MaxValue * _healPercentPerSecond * Time.deltaTime);
    }

    protected override void OnLevelAdd()
    {
        _healPercentPerSecond += _healPercentPerSecondPerLevel;
    }
}
