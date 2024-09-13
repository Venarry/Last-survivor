public class AttackSpeedSkill : SkillBehaviour
{
    private readonly AttackSpeedBuff _attackSpeedBuff = new();
    private readonly CharacterBuffsModel _characterBuffsModel;
    private readonly float _attackCooldownMultiplierPerLevel = 0.1f;
    private readonly float _baseAttackCooldownMultiplier;

    private float AttackCooldownMultiplier => _baseAttackCooldownMultiplier + _attackCooldownMultiplierPerLevel * (CurrentLevel - 1);

    public AttackSpeedSkill(CharacterBuffsModel characterBuffsModel)
    {
        _characterBuffsModel = characterBuffsModel;
    }

    public override SkillTickType SkillTickType => SkillTickType.AwakeTick;
    public override bool HasCooldown => false;

    public override void Apply()
    {
        _characterBuffsModel.Add(_attackSpeedBuff);
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
        return "";
    }
}