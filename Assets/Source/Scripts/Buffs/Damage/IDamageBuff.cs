using Targets;

namespace Buffs.Damage
{
    public interface IDamageBuff : IBuff
    {
        public TargetType TargetType { get; }
        public float ApplyDamage(float damage);
    }
}