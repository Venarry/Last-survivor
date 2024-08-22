using System;

public class MaxHealthUpBuff : IMaxHealthBuff
{
    private float _health;
    private bool _increaseCurrentHealth;
    public Type Type => typeof(MaxHealthUpBuff);
    public bool IsUnique => true;

    public float Apply(float health, out bool increaseCurrentHealth)
    {
        increaseCurrentHealth = _increaseCurrentHealth;
        return health += _health;
    }

    public void SetParamenters(float health, bool increaseCurrentHealth)
    {
        _health = health;
        _increaseCurrentHealth = increaseCurrentHealth;
    }
}
