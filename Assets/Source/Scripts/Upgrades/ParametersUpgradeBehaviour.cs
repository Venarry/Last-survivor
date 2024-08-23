public abstract class ParametersUpgradeBehaviour : IUpgrade
{
    public int MaxLevel { get; private set; } = int.MaxValue;
    public int CurrentLevel { get; private set; } = 0;
    public SkillTickType SkillTickType => SkillTickType.AwakeTick;
    public bool HasCooldown => false;

    public void IncreaseLevel()
    {
        if (CurrentLevel >= MaxLevel)
            return;

        CurrentLevel++;
        OnLevelAdd();
    }

    public abstract void Disable();

    public abstract string GetUpLevelDescription();

    public void IncreaseTimeLeft()
    {
    }

    public virtual void Apply()
    {
    }

    protected virtual void OnLevelAdd()
    {
    }
}
