using System;

public class CritDamageBuff : ICritDamageBuff
{
    public float DamageMultiplier { get; private set; }
    private float _chance;
    public Type Type => typeof(CritDamageBuff);
    public bool CanRepeat => true;

    public bool TryGetCrit(float damage, out float buffedDamage)
    {
        float roll = UnityEngine.Random.Range(0, 101);
        bool isCrit = _chance >= roll;

        buffedDamage = isCrit ? (float)(damage * DamageMultiplier) : (float)damage;

        return isCrit;
    }

    public void SetParameters(float critDamageMultiplier, float chance)
    {
        DamageMultiplier = critDamageMultiplier;
        _chance = chance;
    }
}