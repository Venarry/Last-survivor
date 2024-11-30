namespace Buffs.Experience
{
    public interface IExperienceBuff : IBuff
    {
        public float Apply(float value);
    }
}