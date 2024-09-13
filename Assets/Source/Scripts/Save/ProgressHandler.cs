using System;
using UnityEngine;

public class ProgressHandler : ISaveService
{
    private const string SaveName = "Save";

    private readonly InventoryModel _inventoryModel;
    private readonly LevelsStatisticModel _levelsStatisticModel;
    private readonly CharacterUpgradesModel<ParametersUpgradeBehaviour> _characterUpgrades;
    private SaveData _data;

    public ProgressHandler(
        InventoryModel inventoryModel,
        LevelsStatisticModel levelsStatisticModel,
        CharacterUpgradesModel<ParametersUpgradeBehaviour> characterUpgrades)
    {
        _inventoryModel = inventoryModel;
        _levelsStatisticModel = levelsStatisticModel;
        _characterUpgrades = characterUpgrades;

        _inventoryModel.ItemChanged += OnItemChange;
        _levelsStatisticModel.Added += OnLevelChange;
        _characterUpgrades.Added += OnLevelUpgradeAdd;
    }

    private void OnLevelUpgradeAdd(ParametersUpgradeBehaviour upgrade)
    {

    }

    private void OnLevelChange()
    {
        _data.SetLevels(_levelsStatisticModel.TotalLevel);
    }

    private void OnItemChange(LootType type, int count)
    {
        _data.SetLoot(type, count);
    }

    public void Load()
    {
        if(PlayerPrefs.HasKey(SaveName) == true)
        {
            _data = JsonUtility.FromJson<SaveData>(PlayerPrefs.GetString(SaveName));
            InjectData();
        }
        else
        {
            _data = new();
        }
    }

    private void InjectData()
    {
        foreach (LootData lootData in _data.Loots)
        {
            _inventoryModel.Add(lootData.LootType, lootData.Count);
        }

        _levelsStatisticModel.Set(_data.TotalLevels);
    }

    public void Save()
    {
        string data = JsonUtility.ToJson(_data);

        PlayerPrefs.SetString(SaveName, data);
        Debug.Log(PlayerPrefs.GetString(SaveName));
    }
}
