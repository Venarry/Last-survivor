using UnityEngine;

public class PlayerLootHolder : MonoBehaviour, ILootHolder
{
    private InventoryModel _inventroyModel;
    private ExperienceModel _experienceModel;

    public void Init(InventoryModel inventoryModel, ExperienceModel experienceModel)
    {
        _inventroyModel = inventoryModel;
        _experienceModel = experienceModel;
    }

    public Vector3 ReceivingPosition => transform.position + Vector3.up;

    public void Add(LootType lootType, int count)
    {
        _inventroyModel.Add(lootType, count);
    }

    public void Add(float experience)
    {
        _experienceModel.Add(experience);
    }
}
