using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;

public class CharacterUpgradesModel<T> where T : Upgrade
{
    private readonly Dictionary<Type, T> _upgrades = new();
    private bool _canCast;

    public event Action<T> Added;
    public event Action<Type> Removed;
    public event Action AllRemoved;

    public void OnUpdate()
    {
        foreach (KeyValuePair<Type, T> skill in _upgrades)
        {
            if(skill.Value.SkillTickType == SkillTickType.EveryTick)
            {
                if (_canCast == true)
                {
                    skill.Value.Apply();
                }
            }

            if(skill.Value.HasCooldown == true)
            {
                skill.Value.IncreaseTimeLeft();
            }
        }
    }

    public bool TryAddWithoutIncreaseLevel(T upgrade)
    {
        Type type = upgrade.GetType();

        if (_upgrades.ContainsKey(type) == true)
        {
            return false;
        }

        _upgrades.Add(type, upgrade);

        if (upgrade.SkillTickType == SkillTickType.AwakeTick)
        {
            upgrade.Apply();
        }

        Added?.Invoke(_upgrades[type]);

        return true;
    }

    public void AddWithAwakeIncreaseLevelOrIncreaseLevel(T upgrade)
    {
        Type type = upgrade.GetType();

        if (_upgrades.ContainsKey(type) == false)
        {
            _upgrades.Add(type, upgrade);

            if (upgrade.SkillTickType == SkillTickType.AwakeTick)
            {
                upgrade.Apply();
            }
        }

        _upgrades[type].TryIncreaseLevel();

        Added?.Invoke(_upgrades[type]);
    }

    public bool TryGet(Type type, out T upgrade)
    {
        return _upgrades.TryGetValue(type, out upgrade);
    }

    public T[] GetAll() => _upgrades.Values.ToArray();

    public void Load(T[] upgrades)
    {
        RemoveAll();

        foreach (T upgrade in upgrades)
        {
            _upgrades.Add(upgrade.GetType(), upgrade);

            if (upgrade.SkillTickType == SkillTickType.AwakeTick)
            {
                upgrade.Apply();
            }

            Added?.Invoke(upgrade);
        }
    }

    public void Remove(Type skillType)
    {
        if (_upgrades.ContainsKey(skillType) == false)
            return;

        _upgrades[skillType].Disable();
        _upgrades.Remove(skillType);

        Removed?.Invoke(skillType);
    }

    public void RemoveAll()
    {
        if (_upgrades.Count == 0)
            return;

        foreach (KeyValuePair<Type, T> skill in _upgrades)
        {
            skill.Value.Disable();
        }

        _upgrades.Clear();
        AllRemoved?.Invoke();
    }

    /*public void RemoveWithNotIncluding(params List<Type> types)
    {
        List<Type> upgradesForRemove = new();

        foreach (KeyValuePair<Type, T> upgrade in _upgrades)
        {
            if(types.Contains(upgrade.Key) == false)
            {
                upgradesForRemove.Add(upgrade.Key);
            }
        }

        foreach (Type type in upgradesForRemove)
        {
            _upgrades[type].Disable();
            _upgrades.Remove(type);
        }
    }*/

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

    public bool TryGetUpLevelDescription(Type skillType, out string description)
    {
        description = "";

        if (_upgrades.ContainsKey(skillType) == false)
        {
            return false;
        }

        description = _upgrades[skillType].GetUpLevelDescription();

        return true;
    }

    public void EnableCast()
    {
        _canCast = true;
    }

    public void DisableCast()
    {
        _canCast = false;
    }
}
