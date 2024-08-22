using System;
using UnityEngine;

public abstract class DamageBuff : IDamageBuff
{
    private float _damage;
    public Type Type => typeof(DamageBuff);
    public bool IsUnique => true;
    public abstract TargetType TargetType { get; }

    public float ApplyDamage(float damage)
    {
        return damage += _damage;
    }

    public void SetParameters(float damage)
    {
        _damage = damage;
        Debug.Log($"set {_damage}; {GetHashCode()}");
    }
}
