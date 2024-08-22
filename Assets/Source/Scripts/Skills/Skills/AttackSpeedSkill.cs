public class AttackSpeedSkill : SkillBehaviour
{
    private float _attackCooldownMultiplier;
    private float _attackCooldownMultiplierPerLevel = 0.1f;

    private readonly AttackSpeedBuff _attackSpeedBuff = new();
    private readonly CharacterBuffsModel _characterBuffsModel;

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

    protected override void OnLevelAdd()
    {
        _attackCooldownMultiplier += _attackCooldownMultiplierPerLevel;
        _attackSpeedBuff.SetParameters(_attackCooldownMultiplier);
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
