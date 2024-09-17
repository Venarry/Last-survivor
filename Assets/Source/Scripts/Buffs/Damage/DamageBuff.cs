using System;

public abstract class DamageBuff : IDamageBuff
{
    private float _damage;
    public Type Type => typeof(DamageBuff);
    public bool CanRepeat => true;
    public abstract TargetType TargetType { get; }

    public event Action<IBuff> ParametersChanged;

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
