using System;
using UnityEngine;

public class SwordRoundAttackSkill : SkillBehaviour
{
    private readonly RoundSwordFactory _roundSwordFactory;
    private readonly Transform _spawnTarget;
    private readonly CharacterTargetSearcher _targetSearcher;
    private readonly CooldownTimer _cooldownTimer = new(cooldown: 4);
    private readonly float _damageMultiplier = 0.5f;

    public SwordRoundAttackSkill(
        RoundSwordFactory roundSwordFactory,
        Transform spawnTarget,
        CharacterTargetSearcher targetSearcher)
    {
        _roundSwordFactory = roundSwordFactory;
        _spawnTarget = spawnTarget;
        _targetSearcher = targetSearcher;
    }

    public override UpgradeType UpgradeType => UpgradeType.SwordRoundAttack;
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
            swordSizeText = $"{GameParameters.TextColorStart}{GetSwordSize(CurrentLevel + 1)}{GameParameters.TextColorEnd}";
            swordCountText = $"{GameParameters.TextColorStart}{CurrentLevel + 1}{GameParameters.TextColorEnd}";
        }
        else
        {
            decimal beforeSwordSize = Math.Round((decimal)GetSwordSize(CurrentLevel), 2);
            decimal afterSwordSize = Math.Round((decimal)GetSwordSize(CurrentLevel + 1) - (decimal)GetSwordSize(CurrentLevel), 2);
            swordSizeText = $"{beforeSwordSize} (+{GameParameters.TextColorStart}{afterSwordSize}{GameParameters.TextColorEnd})";

            swordCountText = $"{CurrentLevel} (+{GameParameters.TextColorStart}1{GameParameters.TextColorEnd})";
        }

        return $"Sword count {swordCountText}\n" +
        $"Damage {_damageMultiplier * 100}%\n" +
        $"Sword size {swordSizeText}";
    }

    private float GetSwordSize(int level) => 1 + (float)(level - 1) / 3;
}
