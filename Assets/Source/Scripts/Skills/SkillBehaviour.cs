public abstract class SkillBehaviour : ISkill
{
    public int MaxLevel { get; private set; } = 5;
    public int CurrentLevel { get; private set; } = 1;
    public abstract SkillTickType SkillTickType { get; }
    public abstract bool HasCooldown { get; }

    public void IncreaseLevel()
    {
        if (CurrentLevel >= MaxLevel)
            return;

        CurrentLevel++;

        OnLevelAdd();
    }

    protected virtual void OnLevelAdd()
    {
    }

    public virtual void IncreaseTimeLeft()
    {
    }

    public virtual void TryCast()
    {
    }

    public virtual void Disable()
    {
    }
}
