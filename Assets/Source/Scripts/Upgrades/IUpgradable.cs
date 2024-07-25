public interface IUpgradable<T>
{
    public void Add(T upgrade);
    public void Remove(T upgrade);
    public void RemoveAll();
}