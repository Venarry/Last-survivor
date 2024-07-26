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
    public int MaxLevel { get; private set; } = 5;
    public int CurrentLevel { get; private set; } = 1;

    public void TryCast()
    {
        if(_targetSearcher.TryGetNearestTarget(out _) == false)
        {
            return;
        }

        if (_cooldownTimer.IsReady == true)
        {
            float swordsScale = 1 + (float)(CurrentLevel - 1) / 3;
            _roundSwordFactory.Create(_spawnTarget.position, _spawnTarget, CurrentLevel, swordsScale);
            _cooldownTimer.Reset();
        }
    }

    public void IncreaseLevel()
    {
        if (CurrentLevel >= MaxLevel)
            return;

        CurrentLevel++;
    }

    public void IncreaseTimeLeft()
    {
        _cooldownTimer.Tick();
    }
}
