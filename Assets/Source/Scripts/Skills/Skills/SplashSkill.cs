using UnityEngine;

public class SplashSkill : SkillBehaviour
{
    private PlayerAttackHandler _playerAttackHandler;
    private TargetsProvider _targetsProvider;
    private float _splashAngle = 90;
    private float _splashDistance = 10;
    private float _splashDamageMultiplier = 0.5f;
    private float _splashDamageMultiplierForLevel = 0.2f;

    public SplashSkill(PlayerAttackHandler playerAttackHandler, TargetsProvider targetsProvider)
    {
        _playerAttackHandler = playerAttackHandler;
        _targetsProvider = targetsProvider;
    }

    public override SkillTickType SkillTickType => SkillTickType.AwakeTick;
    public override bool HasCooldown => false;

    public override void TryCast()
    {
        _playerAttackHandler.Attacked += OnAttack;
    }

    public override void Disable()
    {
        _playerAttackHandler.Attacked -= OnAttack;
    }

    protected override void OnLevelAdd()
    {
        _splashDamageMultiplier += _splashDamageMultiplierForLevel;
    }

    private void OnAttack(Target target, float damage)
    {
        Vector3 playerPosition = _playerAttackHandler.transform.position;
        Target[] targets = _targetsProvider.Targets;

        foreach (Target currentTarget in targets)
        {
            if(target == currentTarget)
            {
                continue;
            }

            if (Vector3.Distance(playerPosition, currentTarget.Position) > _splashDistance)
            {
                continue;
            }

            Vector3 targetDirection = (currentTarget.Position - playerPosition).normalized;

            if (Vector3.Angle(_playerAttackHandler.transform.forward, targetDirection) <= _splashAngle / 2)
            {
                currentTarget.TakeDamage(damage * _splashDamageMultiplier);
            }
        }
    }
}
