using System;

namespace Buffs.AttackSpeed
{
    public class AttackSpeedBuff : IAttackSpeedBuff
    {
        private float _attackCooldownMultiplier;

        public event Action<IBuff> ParametersChanged;

        public bool CanRepeat => true;
        public Type Type => typeof(AttackSpeedBuff);

        public float ApplyCooldown(float attackCooldown) =>
            attackCooldown - (attackCooldown * _attackCooldownMultiplier);

        public void SetParameters(float attackCooldownMultiplier)
        {
            _attackCooldownMultiplier = attackCooldownMultiplier;
            ParametersChanged?.Invoke(this);
        }
    }
}