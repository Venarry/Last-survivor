public abstract class Upgrade
{
    public virtual SkillTickType SkillTickType { get; }
    public virtual bool HasCooldown { get; }
    public virtual int MaxLevel { get; }
    public int CurrentLevel { get; private set; }

    public virtual void Apply()
    {
    }

    public virtual void Disable()
    {
    }

    public bool TryIncreaseLevel()
    {
        if (CurrentLevel >= MaxLevel)
            return false;

        CurrentLevel++;
        OnLevelAdd();

        return true;
    }

    protected virtual void OnLevelAdd()
    {
    }

    public virtual void IncreaseTimeLeft()
    {
    }

    public abstract string GetUpLevelDescription();
}