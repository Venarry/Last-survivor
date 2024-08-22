using System;
using System.Collections.Generic;

public class CharacterUpgradesModel<T> where T : IUpgrade
{
    private readonly Dictionary<Type, IUpgrade> _upgrades = new();

    public event Action<IUpgrade> Added;
    public event Action AllRemoved;

    public void OnUpdate()
    {
        foreach (KeyValuePair<Type, IUpgrade> skill in _upgrades)
        {
            if(skill.Value.SkillTickType == SkillTickType.EveryTick)
            {
                skill.Value.Apply();
            }

            if(skill.Value.HasCooldown == true)
            {
                skill.Value.IncreaseTimeLeft();
            }
        }
    }

    public void Add(T skill)
    {
        Type type = skill.GetType();

        if (_upgrades.ContainsKey(type) == false)
        {
            _upgrades.Add(type, skill);

            if (skill.SkillTickType == SkillTickType.AwakeTick)
            {
                skill.Apply();
            }
        }

        _upgrades[type].IncreaseLevel();

        Added?.Invoke(skill);
    }

    public void Remove(Type skillType)
    {
        if (_upgrades.ContainsKey(skillType) == false)
            return;

        _upgrades[skillType].Disable();
        _upgrades.Remove(skillType);
    }

    public void RemoveAll()
    {
        foreach (KeyValuePair<Type, IUpgrade> skill in _upgrades)
        {
            skill.Value.Disable();
        }

        _upgrades.Clear();
        AllRemoved?.Invoke();
    }

    public bool HasUpgrade(Type skillType) => _upgrades.ContainsKey(skillType);

    public bool TryGetUpgradeLevel(Type skillType, out int level, out int maxLevel)
    {
        level = 0;
        maxLevel = 0;

        if(_upgrades.ContainsKey(skillType) == false)
        {
            return false;
        }

        level = _upgrades[skillType].CurrentLevel;
        maxLevel = _upgrades[skillType].MaxLevel;

        return true;
    }

    public string GetUpLevelDescription(Type skillType)
    {
        if (_upgrades.ContainsKey(skillType) == false)
        {
            return "";
        }

        return _upgrades[skillType].GetUpLevelDescription();
    }
}
