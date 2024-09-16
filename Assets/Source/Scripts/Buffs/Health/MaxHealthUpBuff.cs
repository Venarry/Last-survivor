using System;

public class MaxHealthUpBuff : IMaxHealthBuff
{
    private float _health;
    public Type Type => typeof(MaxHealthUpBuff);
    public bool IsUnique => true;

    public float Apply(float health)
    {
        return health += _health;
    }

    public void SetParameters(float health)
    {
        _health = health;
    }
}
