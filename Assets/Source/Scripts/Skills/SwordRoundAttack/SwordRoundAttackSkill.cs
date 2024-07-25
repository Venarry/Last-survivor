using UnityEngine;

public class SwordRoundAttackSkill : ISkill
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

    public SkillTickType SkillTickType => SkillTickType.EveryTick;
    public bool HasCooldown => true;

    public void TryCast()
    {
        if(_targetSearcher.TryGetNearestTarget(out _) == false)
        {
            return;
        }

        if (_cooldownTimer.IsReady == true)
        {
            _roundSwordFactory.Create(_spawnTarget.position, _spawnTarget);
            _cooldownTimer.Reset();
        }
    }

    public void IncreaseLevel()
    {
    }

    public void IncreaseTimeLeft()
    {
        _cooldownTimer.Tick();
    }
}
