public interface IDamageable
{
    public bool IsFriendly { get; }
    public void TakeDamage(int damage);
}
