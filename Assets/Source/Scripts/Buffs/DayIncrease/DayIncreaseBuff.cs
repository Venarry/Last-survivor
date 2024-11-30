using System;

namespace Buffs.DayIncrease
{
    public class DayIncreaseBuff : IDayDurationBuff
    {
        private float _dayDurationIncrease = 0;

        public event Action<IBuff> ParametersChanged;

        public Type Type => GetType();
        public bool CanRepeat => true;

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
}