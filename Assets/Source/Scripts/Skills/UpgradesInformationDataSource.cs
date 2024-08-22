using System;
using System.Collections.Generic;

public class UpgradesInformationDataSource
{
    private readonly Dictionary<Type, string> _skillsName = new()
    {
        [typeof(SwordRoundAttackSkill)] = "Flying swords",
        [typeof(CritAttackSkill)] = "Critical attack",
        [typeof(SplashSkill)] = "Splash",
        [typeof(PassiveHealSkill)] = "Passive heal",
        [typeof(AttackSpeedSkill)] = "Attack speed",
    };

    private readonly Dictionary<Type, string> _skillsDescription = new()
    {
        [typeof(SwordRoundAttackSkill)] = "Spinning swords periodically appear around the character",
        [typeof(CritAttackSkill)] = "Each attack has a chance to deal increased damage",
        [typeof(SplashSkill)] = "When attacking, the character deals damage to neighboring targets",
        [typeof(PassiveHealSkill)] = "The character is passively healing",
        [typeof(AttackSpeedSkill)] = "Reduce attack cooldown",
    };

    public string GetName(Type skillType) => _skillsName[skillType];
    public string GetDescription(Type skillType) => _skillsDescription[skillType];
}
