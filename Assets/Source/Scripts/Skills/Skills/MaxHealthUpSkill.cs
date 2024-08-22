using System.Collections.Generic;
using System.Text;

public class MaxHealthUpSkill : SkillBehaviour
{
    private readonly HealthModel _healthModel;
    private readonly CharacterBuffsModel _characterBuffsModel;
    private readonly MaxHealthUpBuff _maxHealthUpBuff = new();
    private readonly List<float> _healthPerLevel = new() { 25, 50, 80, 120, 160, 250 };
    private float _health;
    private bool _increaseCurrentHealth = true;

    public override int MaxLevel => _healthPerLevel.Count;

    public MaxHealthUpSkill(
        HealthModel healthModel,
        CharacterBuffsModel characterBuffsModel)
    {
        _healthModel = healthModel;
        _characterBuffsModel = characterBuffsModel;
    }

    public override SkillTickType SkillTickType => SkillTickType.AwakeTick;
    public override bool HasCooldown => false;

    public override void Apply()
    {
        _characterBuffsModel.Add(_maxHealthUpBuff);
    }

    protected override void OnLevelAdd()
    {
        _health = _healthPerLevel[CurrentLevel - 1];
        _maxHealthUpBuff.SetParamenters(_health, _increaseCurrentHealth);
        _healthModel.ApplyMaxHealth();
    }

    public override void Disable()
    {
        _characterBuffsModel.Remove(_maxHealthUpBuff);
    }

    public override string GetUpLevelDescription()
    {
        StringBuilder stringBuilder = new("Increase health:\n");

        for (int i = 0; i < _healthPerLevel.Count; i++)
        {
            if(i == CurrentLevel)
            {
                stringBuilder.Append($"{GameParamenters.TextColorStart}{_healthPerLevel[i]}{GameParamenters.TextColorEnd}");
            }
            else
            {
                stringBuilder.Append($"{_healthPerLevel[i]}");
            }

            if(i != _healthPerLevel.Count - 1)
            {
                stringBuilder.Append("/");
            }
        }

        return stringBuilder.ToString();
    }
}
