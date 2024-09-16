using System;
using System.Collections.Generic;

public class SkillsFactory
{
    private readonly CoroutineProvider _coroutineProvider;
    private readonly Player _player;
    private readonly TargetsProvider _targetsProvider;
    private readonly HealthModel _playerHealthModel;
    private readonly CharacterBuffsModel _characterBuffsModel;
    private readonly RoundSwordFactory _roundSwordFactory;
    private readonly ThrowingAxesFactory _throwingAxesFactory;
    private readonly PetFactory _petFactory;
    private readonly Dictionary<UpgradeType, Func<SkillBehaviour>> _skills;

    public SkillsFactory( // бросать топоры вперед. миньон который атакует врагов. вампиризм. взрыв вокруг раз в 10 сек.
        CoroutineProvider coroutineProvider,
        Player player,
        TargetsProvider targetsProvider,
        HealthModel playerHealthModel,
        CharacterBuffsModel characterBuffsModel,
        RoundSwordFactory roundSwordFactory,
        ThrowingAxesFactory throwingAxesFactory,
        PetFactory petFactory)
    {
        _coroutineProvider = coroutineProvider;
        _player = player;
        _targetsProvider = targetsProvider;
        _playerHealthModel = playerHealthModel;
        _characterBuffsModel = characterBuffsModel;
        _roundSwordFactory = roundSwordFactory;
        _throwingAxesFactory = throwingAxesFactory;
        _petFactory = petFactory;

        _skills = new()
        {
            //[UpgradeType.SwordRoundAttack] = CreateSwordRoundAttackSkill,
            [UpgradeType.CritAttack] = CreateCritAttackSkill,
            //[UpgradeType.Splash] = CreateSplashSkill,
            //[UpgradeType.PassiveHealthRegen] = CreatePassiveHealSkill,
            [UpgradeType.AttackCooldownReduce] = CreateAttackSpeedSkill,
            [UpgradeType.MaxHealthUp] = CreateMaxHealthUpSkill,
            //[UpgradeType.ThrowingAxes] = CreateThrowingAxesSkill,
            //[UpgradeType.Pet] = CreatePetSkill,
        };
    }

    public SwordRoundAttackSkill CreateSwordRoundAttackSkill() =>
        new(_roundSwordFactory, _player.transform, _player.TargetSearcher);

    public CritAttackSkill CreateCritAttackSkill() =>
        new(_characterBuffsModel);

    public SplashSkill CreateSplashSkill() =>
        new(_player.AttackHandler, _targetsProvider);

    public PassiveHealSkill CreatePassiveHealSkill() =>
        new(_playerHealthModel);

    public AttackSpeedSkill CreateAttackSpeedSkill() =>
        new(_characterBuffsModel);
    public MaxHealthUpSkill CreateMaxHealthUpSkill() =>
       new(_playerHealthModel, _characterBuffsModel);

    public ThrowingAxesSkill CreateThrowingAxesSkill() =>
       new(_targetsProvider, _throwingAxesFactory, _player.transform, _coroutineProvider);

    public PetSkill CreatePetSkill() =>
       new(_petFactory, _player.transform);

    public SkillBehaviour CreateBy(UpgradeType type, int level)
    {
        SkillBehaviour skill = _skills[type]();
        skill.SetLevel(level);

        return skill;
    }

    public SkillBehaviour[] CreateAllSkills()
    {
        List<SkillBehaviour> skills = new();

        foreach (Func<SkillBehaviour> skill in _skills.Values) 
        {
            skills.Add(skill.Invoke());
        }

        return skills.ToArray();
    }
}
