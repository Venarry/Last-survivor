using System;

namespace Buffs
{
    public interface IBuff
    {
        public event Action<IBuff> ParametersChanged;
        public Type Type { get; }
        public bool CanRepeat { get; }
    }
}