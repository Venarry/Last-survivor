using System.Collections.Generic;
using UnityEngine;

public class InventroyView
{
    private readonly Dictionary<LootType, List<ItemView>> _items = new();

    private readonly InventoryModel _inventoryModel;
    private readonly ItemViewFactory _itemViewFactory;
    private readonly SpritesDataSouce _spritesDataSouce;

    public InventroyView(
        InventoryModel inventoryModel,
        ItemViewFactory itemViewFactory,
        SpritesDataSouce spritesDataSouce)
    {
        _inventoryModel = inventoryModel;
        _itemViewFactory = itemViewFactory;
        _spritesDataSouce = spritesDataSouce;
    }

    public async void SpawnIcons(Transform mainWindowParent, Transform shopParent)
    {
        _inventoryModel.ItemChanged += OnItemChange;

        Dictionary<LootType, Sprite> icons = _spritesDataSouce.GetAllItemsIcon();

        foreach (KeyValuePair<LootType, Sprite> icon in icons)
        {
            _items.Add(icon.Key, new());
            _items[icon.Key].Add(await _itemViewFactory.CreateMainWindowItem(icon.Value, mainWindowParent));
            _items[icon.Key].Add(await _itemViewFactory.CreateShopWindowItem(icon.Value, shopParent));
        }
    }

    public void Add(LootType lootType, int count) =>
        _inventoryModel.Add(lootType, count);

    private void OnItemChange(LootType type, int count)
    {
        List<ItemView> items = _items[type];

        foreach (ItemView item in items)
        {
            item.SetCount(count);
        }
    }
}