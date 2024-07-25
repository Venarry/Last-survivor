using System.Collections;

public interface ISkill : IUpgrade
{
    public SkillTickType SkillTickType { get; }
    public bool HasCooldown { get; }
    public void IncreaseTimeLeft();
}