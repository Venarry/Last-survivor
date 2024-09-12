using System;
using UnityEngine;

public class SaveHandler
{
    private const string SaveName = "Save";

    private readonly InventoryModel _inventoryModel;
    private readonly LevelsStatisticModel _levelsStatisticModel;
    private SaveData _data;

    public SaveHandler(
        InventoryModel inventoryModel,
        LevelsStatisticModel levelsStatisticModel)
    {
        _inventoryModel = inventoryModel;
        _levelsStatisticModel = levelsStatisticModel;

        _inventoryModel.ItemChanged += OnItemChange;
        _levelsStatisticModel.Added += OnLevelChange;
    }

    private void OnLevelChange()
    {
        _data.SetLevels(_levelsStatisticModel.TotalLevel);
        Save();
    }

    private void OnItemChange(LootType type, int count)
    {
        _data.SetLoot(type, count);
        Save();
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

    private void Save()
    {
        string data = JsonUtility.ToJson(_data);

        PlayerPrefs.SetString(SaveName, data);
        Debug.Log(PlayerPrefs.GetString(SaveName));
    }
}
