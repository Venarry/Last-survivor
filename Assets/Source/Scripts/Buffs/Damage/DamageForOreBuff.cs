using Targets;

namespace Buffs.Damage
{
    public class DamageForOreBuff : DamageBuff
    {
        public override TargetType TargetType => TargetType.Ore;
    }
}