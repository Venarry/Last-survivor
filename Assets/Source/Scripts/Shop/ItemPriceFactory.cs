using System.Threading.Tasks;
using UnityEngine;

public class ItemPriceFactory
{
    private readonly AssetsProvider _assetsProvider;
    private readonly SpritesDataSouce _spritesDataSouce;

    public ItemPriceFactory(AssetsProvider assetsProvider, SpritesDataSouce spritesDataSouce)
    {
        _assetsProvider = assetsProvider;
        _spritesDataSouce = spritesDataSouce;
    }

    public async Task Load()
    {
        await _assetsProvider.LoadGameObject<ItemPriceView>(AssetsKeys.ItemPrice);
    }

    public async Task<ItemPriceView> Create(LootType lootType, Transform parent)
    {
        ItemPriceView itemPrice = Object.Instantiate(await _assetsProvider.LoadGameObject<ItemPriceView>(AssetsKeys.ItemPrice), parent);

        Sprite sprite = _spritesDataSouce.Get(lootType);
        itemPrice.SetIcon(sprite);

        return itemPrice;
    }
}
