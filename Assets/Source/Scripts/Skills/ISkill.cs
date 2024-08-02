using System.Collections;

public interface ISkill
{
    public SkillTickType SkillTickType { get; }
    public bool HasCooldown { get; }
    public int MaxLevel { get; }
    public int CurrentLevel { get; }
    public void TryCast();
    public void Disable();
    public void IncreaseLevel();
    public void IncreaseTimeLeft();
}