using System;
using System.Collections.Generic;

public class SkillsFactory
{
    private readonly Player _player;
    private readonly TargetsProvider _targetsProvider;
    private readonly HealthModel _playerHealthModel;
    private readonly RoundSwordFactory _roundSwordFactory;
    private readonly List<Func<ISkill>> _skills;

    public SkillsFactory(
        Player player,
        TargetsProvider targetsProvider,
        HealthModel playerHealthModel,
        RoundSwordFactory roundSwordFactory)
    {
        _player = player;
        _targetsProvider = targetsProvider;
        _roundSwordFactory = roundSwordFactory;
        _playerHealthModel = playerHealthModel;

        _skills = new()
        {
            CreateSwordRoundAttackSkill,
            CreateCritAttackSkill,
            CreateSplashSkill,
            CreatePassiveHealSkill,
        };
    }

    public SwordRoundAttackSkill CreateSwordRoundAttackSkill() =>
        new(_roundSwordFactory, _player.transform, _player.TargetSearcher);

    public CritAttackSkill CreateCritAttackSkill() =>
        new(_player.AttackHandler);

    public SplashSkill CreateSplashSkill() =>
        new(_player.AttackHandler, _targetsProvider);

    public PassiveHealSkill CreatePassiveHealSkill() =>
        new(_playerHealthModel);

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
