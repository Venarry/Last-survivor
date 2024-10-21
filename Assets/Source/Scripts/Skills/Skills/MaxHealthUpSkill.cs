using System;
using System.Collections.Generic;
using YG;

public class MaxHealthUpSkill : SkillBehaviour
{
    private readonly CharacterBuffsModel _characterBuffsModel;
    private readonly MaxHealthUpBuff _maxHealthUpBuff = new();
    private readonly List<float> _healthPerLevel = new() { 25, 50, 80, 120, 160, 250 };
    private float _health;

    public override int MaxLevel => _healthPerLevel.Count;

    public MaxHealthUpSkill(
        CharacterBuffsModel characterBuffsModel)
    {
        _characterBuffsModel = characterBuffsModel;
    }

    public override UpgradeType UpgradeType => UpgradeType.MaxHealthUp;
    public override SkillTickType SkillTickType => SkillTickType.AwakeTick;
    public override bool HasCooldown => false;

    public override void Apply()
    {
        _characterBuffsModel.Add(_maxHealthUpBuff);
        OnLevelChange();
    }

    protected override void OnLevelChange()
    {
        if (CurrentLevel == 0)
            return;

        _health = _healthPerLevel[CurrentLevel - 1];
        _maxHealthUpBuff.SetParameters(_health);

        //_healthModel.TakeDamage(_health * _healthModel.HealthNormalized);
    }

    public override void Disable()
    {
        _characterBuffsModel.Remove(_maxHealthUpBuff);
    }

    public override string GetUpLevelDescription()
    {
        string increaseHealthText;

        switch (YandexGame.lang)
        {
            case GameParameters.CodeRu:
                increaseHealthText = "Увеличение здоровья";
                break;

            case GameParameters.CodeEn:
                increaseHealthText = "Increase health";
                break;

            case GameParameters.CodeTr:
                increaseHealthText = "Sağlığı artırın";
                break;

            default:
                throw new ArgumentNullException(nameof(YandexGame.lang));
        }

        return $"{increaseHealthText}:\n{GetAllLevelsUpgradesText(_healthPerLevel.ToArray()) }";
    }
}
