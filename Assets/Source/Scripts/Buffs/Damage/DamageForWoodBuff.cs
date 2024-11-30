using Targets;

namespace Buffs.Damage
{
    public class DamageForWoodBuff : DamageBuff
    {
        public override TargetType TargetType => TargetType.Wood;
    }
}