using UnityEngine;

public class SwordRoundAttackSkill : ISkill
{
    private RoundSwordFactory _roundSwordFactory;
    private Transform _spawnTarget;
    private TargetSearcher _targetSearcher;
    private CooldownTimer _cooldownTimer = new(4);
    private int _currentLevel = 1;
    private int _maxLevel = 5;

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
            float swordsScale = 1 + (float)(_currentLevel - 1) / 3;
            _roundSwordFactory.Create(_spawnTarget.position, _spawnTarget, _currentLevel, swordsScale);
            _cooldownTimer.Reset();
        }
    }

    public void IncreaseLevel()
    {
        if (_currentLevel >= _maxLevel)
            return;

        _currentLevel++;
    }

    public void IncreaseTimeLeft()
    {
        _cooldownTimer.Tick();
    }
}
