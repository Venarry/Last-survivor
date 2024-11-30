namespace Buffs.AttackSpeed
{
    public interface IAttackSpeedBuff : IBuff
    {
        public float ApplyCooldown(float attackCooldown);
    }
}