using System.Threading.Tasks;
using UnityEngine;

public class ItemPriceFactory
{
    private AssetsProvider _assetsProvider;
    private SpritesDataSouce _spritesDataSouce;

    public ItemPriceFactory(AssetsProvider assetsProvider, SpritesDataSouce spritesDataSouce)
    {
        _assetsProvider = assetsProvider;
        _spritesDataSouce = spritesDataSouce;
    }

    public async Task<ItemPrice> Create(LootType lootType, Transform parent)
    {
        ItemPrice itemPrice = UnityEngine.Object.Instantiate(await _assetsProvider.LoadGameObject<ItemPrice>(AssetsKeys.ItemPrice), parent);

        Sprite sprite = _spritesDataSouce.Get(lootType);
        itemPrice.SetIcon(sprite);

        return itemPrice;
    }
}
