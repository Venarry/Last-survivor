using System;

public class DayIncreaseBuff : IDayDurationBuff
{
    private float _dayDurationIncrease = 0;
    public Type Type => GetType();
    public bool CanRepeat => true;

    public event Action<IBuff> ParametersChanged;

    public float Apply(float dayDuration)
    {
        return _dayDurationIncrease + dayDuration;
    }

    public void SetParameters(float duration)
    {
        _dayDurationIncrease = duration;
        ParametersChanged?.Invoke(this);
    }
}
