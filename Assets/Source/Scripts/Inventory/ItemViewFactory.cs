using System.Threading.Tasks;
using UnityEngine;

public class ItemViewFactory
{
    private readonly AssetsProvider _assetsProvider;
    private readonly SpritesDataSouce _spritesDataSouce;

    public ItemViewFactory(AssetsProvider assetsProvider, SpritesDataSouce spritesDataSouce)
    {
        _assetsProvider = assetsProvider;
        _spritesDataSouce = spritesDataSouce;
    }

    public async Task Load()
    {
        await _assetsProvider.LoadGameObject<ItemView>(AssetsKeys.ItemView);
    }

    public async Task<ItemView> Create(LootType lootType, Transform parent)
    {
        ItemView itemView = Object.Instantiate(await _assetsProvider.LoadGameObject<ItemView>(AssetsKeys.ItemView), parent);

        Sprite icon = _spritesDataSouce.Get(lootType);
        itemView.Init(icon);

        return itemView;
    }

    public async Task<ItemView> Create(Sprite icon, Transform parent)
    {
        ItemView itemView = Object.Instantiate(await _assetsProvider.LoadGameObject<ItemView>(AssetsKeys.ItemView), parent);
        itemView.Init(icon);

        return itemView;
    }
}
