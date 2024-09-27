using System;

public class ExperienceMultiplierBuff : IExperienceBuff
{
    private float _multiplier = 1f;
    public Type Type => GetType();

    public bool CanRepeat => true;

    public event Action<IBuff> ParametersChanged;

    public float Apply(float value) => value * _multiplier;

    public void SetParameters(float multiplier)
    {
        _multiplier = multiplier;
        ParametersChanged?.Invoke(this);
    }
}
