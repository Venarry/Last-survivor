using System;

public class AttackSpeedBuff : IAttackSpeedBuff
{
    private float _attackCooldownMultiplier;
    public Type Type => typeof(AttackSpeedBuff);
    public bool IsUnique => true;

    public float ApplyCooldown(float attackCooldown) =>
        attackCooldown - (attackCooldown * _attackCooldownMultiplier);

    public void SetParameters(float attackCooldownMultiplier)
    {
        _attackCooldownMultiplier = attackCooldownMultiplier;
    }
}
