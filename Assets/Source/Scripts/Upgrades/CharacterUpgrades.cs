using System;
using System.Collections.Generic;

public class CharacterUpgrades : IUpgradable<UpgradeBehaviour>
{
    private readonly Dictionary<Type, UpgradeBehaviour> _upgrades = new();

    public void Add(UpgradeBehaviour upgrade)
    {
        Type type = upgrade.GetType();

        if (_upgrades.ContainsKey(type) == false)
        {
            _upgrades.Add(type, upgrade);
        }

        _upgrades[type].Apply();
    }

    public void Remove(Type upgradeType)
    {
        _upgrades[upgradeType].Cancel();
        _upgrades.Remove(upgradeType);
    }

    public void RemoveAll()
    {
        foreach (KeyValuePair<Type, UpgradeBehaviour> upgrade in _upgrades)
        {
            upgrade.Value.Cancel();
        }

        _upgrades.Clear();
    }
}
