using System;
using Targets;

namespace Buffs.Damage
{
    public class DamageBuff : IDamageBuff
    {
        private float _damage;

        public event Action<IBuff> ParametersChanged;

        public bool CanRepeat => true;
        public Type Type => typeof(DamageBuff);
        public TargetType TargetType { get; }

        public DamageBuff(TargetType targetType)
        {
            TargetType = targetType;
        }

        public float ApplyDamage(float damage)
        {
            return damage += _damage;
        }

        public void SetParameters(float damage)
        {
            _damage = damage;
            ParametersChanged?.Invoke(this);
        }
    }
}