using System;
using System.Collections.Generic;

public class CharacterSkillsModel : IUpgradable<ISkill>
{
    private readonly Dictionary<Type, ISkill> _skills = new();

    public event Action<ISkill> Added;
    public event Action AllRemoved;

    public void OnUpdate()
    {
        foreach (KeyValuePair<Type, ISkill> skill in _skills)
        {
            if(skill.Value.SkillTickType == SkillTickType.EveryTick)
            {
                skill.Value.TryCast();
            }

            if(skill.Value.HasCooldown == true)
            {
                skill.Value.IncreaseTimeLeft();
            }
        }
    }

    public void Add(ISkill skill)
    {
        Type type = skill.GetType();

        if (_skills.ContainsKey(type) == false)
        {
            _skills.Add(type, skill);
            skill.IncreaseLevel();

            if (skill.SkillTickType == SkillTickType.AwakeTick)
            {
                skill.TryCast();
            }
        }
        else
        {
            _skills[type].IncreaseLevel();
        }

        Added?.Invoke(skill);
    }

    public void Remove(Type skillType)
    {
        if (_skills.ContainsKey(skillType) == false)
            return;

        _skills[skillType].Disable();
        _skills.Remove(skillType);
    }

    public void RemoveAll()
    {
        foreach (KeyValuePair<Type, ISkill> skill in _skills)
        {
            skill.Value.Disable();
        }

        _skills.Clear();
        AllRemoved?.Invoke();
    }

    public bool HasSkill(Type skillType) => _skills.ContainsKey(skillType);

    public bool TryGetSkillLevel(Type skillType, out int level, out int maxLevel)
    {
        level = 0;
        maxLevel = 0;

        if(_skills.ContainsKey(skillType) == false)
        {
            return false;
        }

        level = _skills[skillType].CurrentLevel;
        maxLevel = _skills[skillType].MaxLevel;

        return true;
    }

    public string GetSkillUpgradeDescription(Type skillType)
    {
        if (_skills.ContainsKey(skillType) == false)
        {
            return "";
        }

        return _skills[skillType].GetUpgradeDescription();
    }
}
