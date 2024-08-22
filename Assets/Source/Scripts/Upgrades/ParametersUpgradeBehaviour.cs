﻿public abstract class ParametersUpgradeBehaviour : ISkill
{
    public int MaxLevel { get; private set; } = 5;
    public int CurrentLevel { get; private set; } = 0;
    public SkillTickType SkillTickType => SkillTickType.HasNoTick;
    public bool HasCooldown => false;

    public void IncreaseLevel()
    {
        if (CurrentLevel >= MaxLevel)
            return;

        CurrentLevel++;
        OnLevelAdd();
    }

    public abstract void Disable();

    public abstract string GetUpgradeDescription();

    public void IncreaseTimeLeft()
    {
    }

    public void Apply()
    {
    }

    protected virtual void OnLevelAdd()
    {
    }
}
