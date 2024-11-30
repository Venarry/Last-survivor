using System;

namespace Buffs.Experience
{
    public class ExperienceMultiplierBuff : IExperienceBuff
    {
        private float _multiplier = 1f;

        public event Action<IBuff> ParametersChanged;

        public Type Type => GetType();
        public bool CanRepeat => true;

        public float Apply(float value) => value * _multiplier;

        public void SetParameters(float multiplier)
        {
            _multiplier = multiplier;
            ParametersChanged?.Invoke(this);
        }
    }
}