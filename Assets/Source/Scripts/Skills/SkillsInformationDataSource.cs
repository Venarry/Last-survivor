using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsInformationDataSource : MonoBehaviour
{
    private Dictionary<Type, string> _skillsName = new()
    {
        [typeof(SwordRoundAttackSkill)] = "Flying swords",
        [typeof(CritAttackSkill)] = "Critical attack",
        [typeof(SplashSkill)] = "Splash",
        [typeof(PassiveHealSkill)] = "Passive heal",
    };

    private Dictionary<Type, string> _skillsDescription = new()
    {
        [typeof(SwordRoundAttackSkill)] = "Spinning swords periodically appear around the character",
        [typeof(CritAttackSkill)] = "Each attack has a chance to deal increased damage",
        [typeof(SplashSkill)] = "When attacking, the character deals damage to neighboring targets",
        [typeof(PassiveHealSkill)] = "The character is passively healing",
    };

    public string GetName(Type skillType) => _skillsName[skillType];
    public string GetDescription(Type skillType) => _skillsDescription[skillType];
}
