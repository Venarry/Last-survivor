using UnityEngine;

public class SwordRoundAttackSkill : SkillBehaviour
{
    private RoundSwordFactory _roundSwordFactory;
    private Transform _spawnTarget;
    private TargetSearcher _targetSearcher;
    private CooldownTimer _cooldownTimer = new(4);

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

    public override async void TryCast()
    {
        if(_targetSearcher.TryGetNearestTarget(out _) == false)
        {
            return;
        }

        if (_cooldownTimer.IsReady == true)
        {
            float swordsScale = 1 + (float)(CurrentLevel - 1) / 3;
            await _roundSwordFactory.Create(_spawnTarget.position, _spawnTarget, CurrentLevel, swordsScale);
            _cooldownTimer.Reset();
        }
    }

    public override void IncreaseTimeLeft()
    {
        _cooldownTimer.Tick();
    }
}
