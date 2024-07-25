﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSkills : MonoBehaviour, IUpgradable<ISkill>
{
    private readonly Dictionary<Type, ISkill> _skills = new();

    private void Update()
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
        }
        else
        {
            _skills[type].IncreaseLevel();
        }
    }

    public void Remove(ISkill skill)
    {
        _skills.Remove(skill.GetType());
    }

    public void RemoveAll()
    {
        _skills.Clear();
    }
}