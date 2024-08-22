using System.Collections.Generic;

public class MaxHealthUpSkill : SkillBehaviour
{
    private readonly HealthModel _healthModel;
    private readonly CharacterBuffsModel _characterBuffsModel;
    private readonly MaxHealthUpBuff _maxHealthUpBuff = new();
    private readonly List<float> _healthPerLevel = new() { 30, 50, 70, 110, 150, 250 };
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
        _health = _healthPerLevel[CurrentLevel];
        _maxHealthUpBuff.SetParamenters(_health, _increaseCurrentHealth);
        _healthModel.ApplyMaxHealth();
    }

    public override void Disable()
    {
        _characterBuffsModel.Remove(_maxHealthUpBuff);
    }

    public override string GetUpLevelDescription()
    {
        return "";
    }
}
