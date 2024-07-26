using System;
using System.Collections.Generic;

public class SkillsFactory
{
    private readonly Player _player;
    private readonly RoundSwordFactory _roundSwordFactory;
    private List<Func<ISkill>> _skills;

    public SkillsFactory(Player player, RoundSwordFactory roundSwordFactory)
    {
        _player = player;
        _roundSwordFactory = roundSwordFactory;

        _skills = new()
        {
            CreateSwordRoundAttack,
        };
    }

    public SwordRoundAttackSkill CreateSwordRoundAttack()
    {
        return new(_roundSwordFactory, _player.transform, _player.TargetSearcher);
    }

    public ISkill[] CreateAllSkills()
    {
        List<ISkill> skills = new();

        foreach (Func<ISkill> skill in _skills) 
        {
            skills.Add(skill.Invoke());
        }

        return skills.ToArray();
    }
}
