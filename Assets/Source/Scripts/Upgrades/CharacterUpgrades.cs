using System;
using System.Collections.Generic;

public class CharacterUpgrades : IUpgradable<IUpgrade>
{
    private readonly Dictionary<Type, IUpgrade> _upgrades = new();

    public void Add(IUpgrade upgrade)
    {
        Type type = upgrade.GetType();

        if (_upgrades.ContainsKey(type) == false)
        {
            _upgrades.Add(type, upgrade);
            _upgrades[type].Apply();
        }
        else
        {
            _upgrades[type].IncreaseLevel();
        }
    }

    public void Remove(Type upgradeType)
    {
        _upgrades[upgradeType].Cancel();
        _upgrades.Remove(upgradeType);
    }

    public void RemoveAll()
    {
        foreach (KeyValuePair<Type, IUpgrade> upgrade in _upgrades)
        {
            upgrade.Value.Cancel();
        }

        _upgrades.Clear();
    }
}
