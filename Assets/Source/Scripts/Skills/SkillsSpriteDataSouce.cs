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
            { typeof(SwordRoundAttackSkill), Resources.Load<Sprite>(ResourcesPath.SkillIconSwordRoundAttack) }
        };
    }

    public Sprite Get(Type skillType)
    {
        return _icons[skillType];
    }
}
