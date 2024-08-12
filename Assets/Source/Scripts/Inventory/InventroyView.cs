using System;
using UnityEngine;
using UnityEngine.UI;

public class InventroyView : MonoBehaviour
{
    private InventoryModel _inventoryModel;

    public void Init(InventoryModel inventoryModel)
    {
        _inventoryModel = inventoryModel;

        _inventoryModel.ItemAdded += OnItemAdd;
    }

    public void Add(LootType lootType, int count) =>
        _inventoryModel.Add(lootType, count);

    private void OnItemAdd(LootType type, int count)
    {

    }
}

public class ItemView : MonoBehaviour
{
    [SerializeField] private Image _
}
