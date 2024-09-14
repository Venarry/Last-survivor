public abstract class Upgrade
{
    public abstract UpgradeType UpgradeType { get; }
    public virtual SkillTickType SkillTickType { get; }
    public virtual bool HasCooldown { get; }
    public virtual int MaxLevel { get; }
    public int CurrentLevel { get; private set; }

    public void SetLevel(int level) =>
        CurrentLevel = level;

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
        OnLevelChange();

        return true;
    }

    protected virtual void OnLevelChange()
    {
    }

    public virtual void IncreaseTimeLeft()
    {
    }

    public abstract string GetUpLevelDescription();
}
