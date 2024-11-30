namespace Buffs.DayIncrease
{
    public interface IDayDurationBuff : IBuff
    {
        public float Apply(float dayDuration);
    }
}