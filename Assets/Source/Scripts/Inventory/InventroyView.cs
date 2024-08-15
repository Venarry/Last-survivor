using System.Collections.Generic;
using UnityEngine;

public class InventroyView : MonoBehaviour
{
    private readonly Dictionary<LootType, ItemView> _items = new();

    private InventoryModel _inventoryModel;
    private ItemViewFactory _itemViewFactory;
    private SpritesDataSouce _spritesDataSouce;

    public async void Init(InventoryModel inventoryModel, ItemViewFactory itemViewFactory, SpritesDataSouce spritesDataSouce, Transform parent)
    {
        _inventoryModel = inventoryModel;
        _itemViewFactory = itemViewFactory;
        _spritesDataSouce = spritesDataSouce;

        _inventoryModel.ItemChanged += OnItemAdd;

        Dictionary<LootType, Sprite> icons = _spritesDataSouce.GetAllItemsIcon();

        foreach (KeyValuePair<LootType, Sprite> icon in icons)
        {
            _items.Add(icon.Key, await _itemViewFactory.Create(icon.Value, parent));
        }
    }

    public void Add(LootType lootType, int count) =>
        _inventoryModel.Add(lootType, count);

    private void OnItemAdd(LootType type, int count)
    {
        _items[type].SetCount(count);
    }
}