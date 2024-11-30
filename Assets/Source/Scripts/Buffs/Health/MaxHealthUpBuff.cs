using System;

namespace Buffs.Health
{
    public class MaxHealthUpBuff : IMaxHealthBuff
    {
        private float _health;

        public event Action<IBuff> ParametersChanged;

        public Type Type => typeof(MaxHealthUpBuff);
        public bool CanRepeat => true;

        public float Apply(float health)
        {
            return health += _health;
        }

        public void SetParameters(float health)
        {
            _health = health;
            ParametersChanged?.Invoke(this);
        }
    }
}