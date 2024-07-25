using UnityEngine;

public class SwordRoundAttackSkill : ISkill
{
    private RoundSwordFactory _roundSwordFactory;
    private Transform _spawnTarget;
    private CooldownTimer _cooldownTimer = new(4);

    public SwordRoundAttackSkill(
        RoundSwordFactory roundSwordFactory,
        Transform spawnTarget)
    {
        _roundSwordFactory = roundSwordFactory;
        _spawnTarget = spawnTarget;
    }

    public SkillTickType SkillTickType => SkillTickType.EveryTick;
    public bool HasCooldown => true;

    public void Apply()
    {
        if (_cooldownTimer.IsReady == true)
        {
            _roundSwordFactory.Create(_spawnTarget.position, _spawnTarget);
            _cooldownTimer.Reset();
        }
    }

    public void Cancel()
    {
    }

    public void IncreaseLevel()
    {
    }

    public void IncreaseTimeLeft()
    {
        _cooldownTimer.Tick();
    }
}
