namespace Buffs.Health
{
    public interface IMaxHealthBuff : IBuff
    {
        public float Apply(float health);
    }
}