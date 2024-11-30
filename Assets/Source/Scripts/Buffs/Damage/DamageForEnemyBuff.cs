using Targets;

namespace Buffs.Damage
{
    public class DamageForEnemyBuff : DamageBuff
    {
        public override TargetType TargetType => TargetType.Enemy;
    }
}