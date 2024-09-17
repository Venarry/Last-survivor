using System;

public interface IBuff
{
    public Type Type { get; }
    public bool CanRepeat { get; }
    public event Action<IBuff> ParametersChanged;
}
