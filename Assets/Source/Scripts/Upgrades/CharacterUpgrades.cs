using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterUpgrades : IUpgradable
{
    private readonly Dictionary<Type, IUpgrade> _upgrades = new();

    public void Add(IUpgrade upgrade)
    {
        Type type = upgrade.GetType();

        if (_upgrades.ContainsKey(type) == false)
        {
            _upgrades.Add(type, upgrade);
            upgrade.IncreaseLevel();
            upgrade.Apply();
        }
        else
        {
            upgrade.Cancel();
            upgrade.IncreaseLevel();
            upgrade.Apply();
        }
    }

    public void Remove(IUpgrade upgrade)
    {
        upgrade.Cancel();
        _upgrades.Remove(upgrade.GetType());
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
