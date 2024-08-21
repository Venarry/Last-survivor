using UnityEngine;

public class PlayerLootHolder : MonoBehaviour, ILootHolder
{
    private InventroyView _inventroyView;
    private ExperienceView _experienceView;

    private void Awake()
    {
        _inventroyView = GetComponent<InventroyView>();
        _experienceView = GetComponent<ExperienceView>();
    }

    public Vector3 ReceivingPosition => transform.position + Vector3.up;

    public void Add(LootType lootType, int count)
    {
        _inventroyView.Add(lootType, count);
    }

    public void Add(float experience)
    {
        _experienceView.Add(experience);
    }
}
