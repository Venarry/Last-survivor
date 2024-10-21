using System;
using System.Text;

public abstract class SkillBehaviour : Upgrade
{
    public override int MaxLevel { get; } = 5;

    protected string GetAllLevelsUpgradesText(float[] values)
    {
        StringBuilder stringBuilder = new();

        for (int i = 0; i < MaxLevel; i++)
        {
            if (CurrentLevel == 0 || i != CurrentLevel - 1)
            {
                stringBuilder.Append($"{Math.Round(values[i], 1)}");
            }
            else
            {
                stringBuilder.Append($"{Decorate(Math.Round(values[i], 1).ToString())}");
            }

            if (i != MaxLevel - 1)
            {
                stringBuilder.Append("/");
            }
        }

        return stringBuilder.ToString();
    }
}
