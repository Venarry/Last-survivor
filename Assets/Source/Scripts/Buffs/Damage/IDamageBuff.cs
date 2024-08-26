public interface IDamageBuff : IBuff
{
    public TargetType TargetType { get; }
    public float ApplyDamage(float damage);
}
