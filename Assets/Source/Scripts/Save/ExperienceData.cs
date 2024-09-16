using System;

[Serializable]
public class ExperienceData
{
    public int Level;
    public float Experience;

    public ExperienceData(int level, float experience)
    {
        Level = level;
        Experience = experience;
    }
}
