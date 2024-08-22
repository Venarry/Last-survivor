using System;
using UnityEngine;

public class SwordRoundAttackSkill : SkillBehaviour
{
    private readonly RoundSwordFactory _roundSwordFactory;
    private readonly Transform _spawnTarget;
    private readonly TargetSearcher _targetSearcher;
    private readonly CooldownTimer _cooldownTimer = new(cooldown: 4);
    private readonly float _damageMultiplier = 0.6f;

    public SwordRoundAttackSkill(
        RoundSwordFactory roundSwordFactory,
        Transform spawnTarget,
        TargetSearcher targetSearcher)
    {
        _roundSwordFactory = roundSwordFactory;
        _spawnTarget = spawnTarget;
        _targetSearcher = targetSearcher;
    }

    public override SkillTickType SkillTickType => SkillTickType.EveryTick;
    public override bool HasCooldown => true;

    public override async void Apply()
    {
        if(_targetSearcher.TryGetNearestTarget(out _) == false)
        {
            return;
        }

        if (_cooldownTimer.IsReady == true)
        {
            float swordSize = GetSwordSize(CurrentLevel);
            _cooldownTimer.Reset();
            await _roundSwordFactory.Create(_spawnTarget.position, _spawnTarget, CurrentLevel, _damageMultiplier, swordSize);
        }
    }

    public override void IncreaseTimeLeft()
    {
        _cooldownTimer.Tick();
    }

    public override string GetUpLevelDescription() 
    {
        string swordSizeText;
        string swordCountText;

        if(CurrentLevel == 0)
        {
            swordSizeText = $"{GameParamenters.TextColorStart}{GetSwordSize(CurrentLevel + 1)}{GameParamenters.TextColorEnd}";
            swordCountText = $"{GameParamenters.TextColorStart}{CurrentLevel + 1}{GameParamenters.TextColorEnd}";
        }
        else
        {
            decimal beforeSwordSize = Math.Round((decimal)GetSwordSize(CurrentLevel), 2);
            decimal afterSwordSize = Math.Round((decimal)GetSwordSize(CurrentLevel + 1) - (decimal)GetSwordSize(CurrentLevel), 2);
            swordSizeText = $"{beforeSwordSize} (+{GameParamenters.TextColorStart}{afterSwordSize}{GameParamenters.TextColorEnd})";

            swordCountText = $"{CurrentLevel} (+{GameParamenters.TextColorStart}1{GameParamenters.TextColorEnd})";
        }

        return $"Sword count {swordCountText}\n" +
        $"Damage {_damageMultiplier * 100}%\n" +
        $"Sword size {swordSizeText}";
    }

    private float GetSwordSize(int level) => 1 + (float)(level - 1) / 3;
}
