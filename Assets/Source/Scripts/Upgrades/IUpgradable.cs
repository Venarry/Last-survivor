public interface IUpgradable
{
    public void Add(IUpgrade upgrade);
    public void Remove(IUpgrade upgrade);
    public void RemoveAll();
}