using System.Collections;

public interface ISkill
{
    public SkillTickType SkillTickType { get; }
    public bool HasCooldown { get; }
    public void TryCast();
    public void IncreaseLevel();
    public void IncreaseTimeLeft();
}