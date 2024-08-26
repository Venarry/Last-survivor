public interface ICritDamageBuff : IBuff
{
    public float DamageMultiplier { get; }
    public bool TryGetCrit(float damage, out float buffedDamage);
}
