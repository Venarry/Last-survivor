public abstract class SkillBehaviour : IUpgrade
{
    public int MaxLevel { get; private set; } = 5;
    public int CurrentLevel { get; private set; } = 0;
    public abstract SkillTickType SkillTickType { get; }
    public abstract bool HasCooldown { get; }

    public void IncreaseLevel()
    {
        if (CurrentLevel >= MaxLevel)
            return;

        CurrentLevel++;

        if(CurrentLevel > 1)
        {
            OnLevelAdd();
        }
    }

    public abstract string GetUpLevelDescription();

    public virtual void IncreaseTimeLeft()
    {
    }

    public abstract void Apply();

    public virtual void Disable()
    {
    }

    protected virtual void OnLevelAdd()
    {
    }
}
