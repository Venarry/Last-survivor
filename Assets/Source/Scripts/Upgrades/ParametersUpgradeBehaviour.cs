public abstract class ParametersUpgradeBehaviour : Upgrade
{
    public override int MaxLevel { get; } = int.MaxValue;
    public override SkillTickType SkillTickType => SkillTickType.AwakeTick;
    public override bool HasCooldown => false;
}
