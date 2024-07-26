using System;

public interface IUpgradable<T>
{
    public void Add(T upgrade);
    public void Remove(Type upgrade);
    public void RemoveAll();
}