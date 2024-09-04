using System.Text;

public abstract class SkillBehaviour : IUpgrade
{
    public virtual int MaxLevel { get; private set; } = 5;
    public int CurrentLevel { get; private set; } = 0;
    public abstract SkillTickType SkillTickType { get; }
    public abstract bool HasCooldown { get; }

    public void IncreaseLevel()
    {
        if (CurrentLevel >= MaxLevel)
            return;

        CurrentLevel++;
        OnLevelAdd();
    }

    public abstract string GetUpLevelDescription();

    public virtual void IncreaseTimeLeft()
    {
    }

    public virtual void Apply()
    {
    }

    public virtual void Disable()
    {
    }

    protected virtual void OnLevelAdd()
    {
    }

    protected string GetAllLevelsUpgradesText(float[] values)
    {
        StringBuilder stringBuilder = new();

        for (int i = 0; i < MaxLevel; i++)
        {
            if (CurrentLevel == 0 || i != CurrentLevel - 1)
            {
                stringBuilder.Append($"{values[i]}");
            }
            else
            {
                stringBuilder.Append($"{GameParamenters.TextColorStart}{values[i]}{GameParamenters.TextColorEnd}");
            }

            if (i != MaxLevel - 1)
            {
                stringBuilder.Append("/");
            }
        }

        return stringBuilder.ToString();
    }
}
