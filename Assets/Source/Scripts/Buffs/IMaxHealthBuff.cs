public interface IMaxHealthBuff : IBuff
{
    public float Apply(float health, out bool increaseCurrentHealth);
}
