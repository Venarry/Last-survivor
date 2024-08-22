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

    public float HealthNormalized => (float)Value / MaxValue;

    public event Action HealthChanged;
    public event Action HealthOver;

    public void Restore()
    {
        Value = MaxValue;
        HealthChanged?.Invoke();
    }

    public void SetHealth(float value)
    {
        if (value < 0)
        {
            value = 0;
        }

        Value = value;
        HealthChanged?.Invoke();

        if (Value <= 0)
            HealthOver?.Invoke();
    }

    public void AddMaxHealh(float value, bool increaseCurrentHealth)
    {
        SetMaxHealth(MaxValue + value);

        if (increaseCurrentHealth == false)
            return;

        Value += value;
        HealthChanged?.Invoke();
    }

    public void SetMaxHealth(float value)
    {
        if (value < 1)
            value = 1;

        _baseMaxValue = value;
    }

    public void TakeDamage(float value)
    {
        if (Value <= 0)
            return;

        if (value < 0)
            value = 0;

        Value -= value;
        HealthChanged?.Invoke();

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

        HealthChanged?.Invoke();
    }

    public void ApplyMaxHealth()
    {
        MaxValue = _baseMaxValue;
        IMaxHealthBuff[] buffs = _characterBuffsModel.GetBuffs<IMaxHealthBuff>();

        foreach (IMaxHealthBuff buff in buffs)
        {
            float startHealth = MaxValue;
            MaxValue = buff.Apply(MaxValue, out bool increaseCurrentHealth);

            /*if (increaseCurrentHealth == true)
            {
                Value += MaxValue - startHealth;
            }
            else if (Value > MaxValue)
            {
                Value = MaxValue;
            }*/
        }

        Debug.Log($"{Value} {MaxValue}");
        HealthChanged?.Invoke();
    }
}
