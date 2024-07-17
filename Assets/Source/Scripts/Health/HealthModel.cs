using System;

public class HealthModel
{
    public event Action HealthChanged;
    public event Action HealthOver;

    public HealthModel(float maxValue)
    {
        MaxValue = maxValue;
        Value = maxValue;
    }

    public HealthModel(float maxValue, float value)
    {
        MaxValue = maxValue;
        Value = value;
    }

    public float Value { get; private set; }
    public float MaxValue { get; private set; }

    public float HealthNormalized => (float)Value / MaxValue;

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

    public void SetMaxHealth(float value)
    {
        if (value < 1)
            value = 1;

        MaxValue = value;
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
}
