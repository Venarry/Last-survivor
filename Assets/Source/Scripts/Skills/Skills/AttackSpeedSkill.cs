using System;
using YG;

public class AttackSpeedSkill : SkillBehaviour
{
    private readonly AttackSpeedBuff _attackSpeedBuff = new();
    private readonly CharacterBuffsModel _characterBuffsModel;
    private readonly float _attackCooldownMultiplierPerLevel = 0.1f;

    private float AttackCooldownMultiplier => _attackCooldownMultiplierPerLevel * CurrentLevel;

    public AttackSpeedSkill(CharacterBuffsModel characterBuffsModel)
    {
        _characterBuffsModel = characterBuffsModel;
    }

    public override UpgradeType UpgradeType => UpgradeType.AttackCooldownReduce;
    public override SkillTickType SkillTickType => SkillTickType.AwakeTick;
    public override bool HasCooldown => false;

    public override void Apply()
    {
        _characterBuffsModel.Add(_attackSpeedBuff);
        _attackSpeedBuff.SetParameters(AttackCooldownMultiplier);
    }

    protected override void OnLevelChange()
    {
        _attackSpeedBuff.SetParameters(AttackCooldownMultiplier);
    }

    public override void Disable()
    {
        _characterBuffsModel.Remove(_attackSpeedBuff);
    }

    public override string GetUpLevelDescription()
    {
        string attackCooldownText;

        switch (YandexGame.lang)
        {
            case GameParameters.CodeRu:
                attackCooldownText = "Уменьшение время между атакми";
                break;

            case GameParameters.CodeTr:
                attackCooldownText = "Sağlığı artırın";
                break;

            default:
                attackCooldownText = "Increase health";
                break;
        }

        return $"{attackCooldownText}: {AttackCooldownMultiplier} + {Decorate(_attackCooldownMultiplierPerLevel.ToString())}";
    }
}