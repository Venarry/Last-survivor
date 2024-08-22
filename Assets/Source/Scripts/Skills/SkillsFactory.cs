using System;
using System.Collections.Generic;

public class SkillsFactory
{
    private readonly Player _player;
    private readonly TargetsProvider _targetsProvider;
    private readonly HealthModel _playerHealthModel;
    private readonly CharacterBuffsModel _characterBuffsModel;
    private readonly RoundSwordFactory _roundSwordFactory;
    private readonly List<Func<SkillBehaviour>> _skills;

    public SkillsFactory( // бросать топоры вперед. миньон который атакует врагов. вампиризм. взрыв вокруг раз в 10 сек. увеличенный атакспид
        Player player,
        TargetsProvider targetsProvider,
        HealthModel playerHealthModel,
        CharacterBuffsModel characterBuffsModel,
        RoundSwordFactory roundSwordFactory)
    {
        _player = player;
        _targetsProvider = targetsProvider;
        _roundSwordFactory = roundSwordFactory;
        _playerHealthModel = playerHealthModel;
        _characterBuffsModel = characterBuffsModel;

        _skills = new()
        {
            CreateSwordRoundAttackSkill,
            CreateCritAttackSkill,
            CreateSplashSkill,
            CreatePassiveHealSkill,
            CreateAttackSpeedSkill,
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

    public AttackSpeedSkill CreateAttackSpeedSkill() =>
        new(_characterBuffsModel);

    public SkillBehaviour[] CreateAllSkills()
    {
        List<SkillBehaviour> skills = new();

        foreach (Func<SkillBehaviour> skill in _skills) 
        {
            skills.Add(skill.Invoke());
        }

        return skills.ToArray();
    }
}
