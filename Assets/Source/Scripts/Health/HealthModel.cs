using System;
using UnityEngine;

public class HealthModel
{
    private readonly CharacterBuffsModel _characterBuffsModel;
    private float _baseMaxValue;

    public HealthModel(CharacterBuffsModel characterBuffsModel, float maxValue)
    {
        _characterBuffsModel = characterBuffsModel;
        _baseMaxValue = maxValue;
        MaxValue = maxValue;
        Value = maxValue;
    }

    public HealthModel(CharacterBuffsModel characterBuffsModel, float maxValue, float value)
    {
        _characterBuffsModel = characterBuffsModel;
        _baseMaxValue = maxValue;
        MaxValue = maxValue;
        Value = value;
    }

    public float Value { get; private set; }
    public float MaxValue { get; private set; }
    public float BaseMaxValue => _baseMaxValue;
    public float HealthNormalized => (float)Value / MaxValue;

    public event Action Changed;
    public event Action DamageReceived;
    public event Action HealthOver;

    public void Restore()
    {
        Value = MaxValue;
        Changed?.Invoke();
    }

    public void SetMaxHealth(float value)
    {
        if (value < 1)
            value = 1;

        _baseMaxValue = value;
        ApplyMaxHealthBuffs();
    }

    public void TakeDamage(float value)
    {
        if (Value <= 0)
            return;

        if (value < 0)
            value = 0;

        Value -= value;
        Changed?.Invoke();
        DamageReceived?.Invoke();

        if (Value <= 0)
        {
            Value = 0;
            HealthOver?.Invoke();
        }
    }

    public void Add(float value)
    {
        if(value < 0)
            value = 0;

        Value += value;

        if(Value > MaxValue)
            Value = MaxValue;

        Changed?.Invoke();
    }

    public void ApplyMaxHealthBuffs()
    {
        float healthMultiplier = HealthNormalized;
        //float healthBeforeReset = Value;
        MaxValue = _baseMaxValue;
        Value = MaxValue * healthMultiplier;

        IMaxHealthBuff[] buffs = _characterBuffsModel.GetBuffs<IMaxHealthBuff>();

        if (buffs.Length == 0)
            return;

        foreach (IMaxHealthBuff buff in buffs)
        {
            float bufferHealth = MaxValue;
            MaxValue = buff.Apply(MaxValue);
            //float maxHealthMultiplier = MaxValue / bufferHealth;
            float deltaMaxHealth = MaxValue - bufferHealth;
            float deltaMaxHealthWithMultiplier = deltaMaxHealth * healthMultiplier;

            Value += deltaMaxHealthWithMultiplier;

            healthMultiplier = HealthNormalized;
        }

        if(Value > MaxValue)
        {
            Value = MaxValue;
        }

        if(Value < 1)
        {
            Value = 1;
        }

        Debug.Log(Value);
        Changed?.Invoke();
    }

    public void SetNormalizedHealth(float multiplier)
    {
        Value = MaxValue * multiplier;
        Debug.Log(Value);

        Changed?.Invoke();
    }
}
