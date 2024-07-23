using UnityEngine;

public class InventroyView : MonoBehaviour
{
    private InventoryModel _inventoryModel;

    public void Init(InventoryModel inventoryModel)
    {
        _inventoryModel = inventoryModel;
    }

    public void Add(LootType lootType, int count) =>
        _inventoryModel.Add(lootType, count);
}
