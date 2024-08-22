using System;

public interface IUpgradable<T>
{
    public void Add(T upgrade);
    public void Remove(Type upgrade);
    public void RemoveAll();
    public bool HasSkill(Type type);
    public bool TryGetSkillLevel(Type skillType, out int level, out int maxLevel);
    public string GetSkillUpgradeDescription(Type skillType);
}