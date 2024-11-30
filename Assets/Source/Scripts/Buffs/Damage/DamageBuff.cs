using System;
using Targets;

namespace Buffs.Damage
{
    public abstract class DamageBuff : IDamageBuff
    {
        private float _damage;

        public event Action<IBuff> ParametersChanged;

        public bool CanRepeat => true;
        public Type Type => typeof(DamageBuff);
        public abstract TargetType TargetType { get; }

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