using System;
using System.Collections.Generic;
using UnityEngine;

public class SkillsSpriteDataSouce
{
    private readonly Dictionary<Type, Sprite> _icons;

    public SkillsSpriteDataSouce()
    {
        _icons = new()
        {
            { typeof(SwordRoundAttackSkill), Resources.Load<Sprite>(ResourcesPath.SwordRoundAttackSkillIcon) }
        };
    }

    public Sprite Get(ISkill skill)
    {
        return _icons[skill.GetType()];
    }
}
