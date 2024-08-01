using System.Threading.Tasks;
using UnityEngine;

public abstract class LootFactory
{
    private readonly ILootHolder _lootHolder;
    protected AssetsProvider AssetsProvider;
    protected abstract string AssetKey { get; }
    protected abstract LootType LootType { get; }

    protected LootFactory(ILootHolder lootHolder, AssetsProvider assetsProvider)
    {
        _lootHolder = lootHolder;
        AssetsProvider = assetsProvider;
    }

    public async Task<Loot> Create(Vector3 position)
    {
        Loot loot = Object.Instantiate(await AssetsProvider.LoadGameObject<Loot>(AssetKey), position, Quaternion.identity);

        int reward = 1;
        int experience = 1;
        loot.Init(reward, experience, LootType, _lootHolder);
        
        return loot;
    }
}