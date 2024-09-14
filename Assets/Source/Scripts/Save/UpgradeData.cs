using System;

[Serializable]
public class UpgradeData
{
    public UpgradeType Type;
    public int Level;

    public UpgradeData(UpgradeType type, int level)
    {
        Type = type;
        Level = level;
    }
}