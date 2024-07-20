using UnityEngine;

public class InventroyView : MonoBehaviour, ILootHolder
{
    private InventoryModel _inventoryModel;

    public Vector3 ReceivingPosition => transform.position + Vector3.up;

    public void Init(InventoryModel inventoryModel)
    {
        _inventoryModel = inventoryModel;
    }

    public void Add(LootType lootType, int count) =>
        _inventoryModel.Add(lootType, count);
}
