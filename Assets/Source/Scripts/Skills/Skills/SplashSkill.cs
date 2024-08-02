using UnityEngine;

public class SplashSkill : SkillBehaviour
{
    private PlayerAttackHandler _playerAttackHandler;

    public SplashSkill(PlayerAttackHandler playerAttackHandler)
    {
        _playerAttackHandler = playerAttackHandler;
    }

    public override SkillTickType SkillTickType => SkillTickType.AwakeTick;
    public override bool HasCooldown => false;

    public override void TryCast()
    {
        _playerAttackHandler.Attacked += OnAttack;
    }

    private void OnAttack(Target target, float damage)
    {
        Vector3 playerPosition = _playerAttackHandler.transform.position;
        Vector3 targetDirection = (target.Position - playerPosition).normalized;

        Debug.Log(Vector3.Angle(playerPosition, targetDirection));
    }
}
