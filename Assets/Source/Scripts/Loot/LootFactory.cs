using System.Threading.Tasks;
using UnityEngine;

public abstract class LootFactory : ObjectPoolBehaviour<Loot>
{
    private readonly ILootHolder _lootHolder;
    protected AssetsProvider AssetsProvider;

    protected virtual int RewardCount { get; } = 1;
    protected virtual int ExperienceCount { get; } = 1;

    protected LootFactory(ILootHolder lootHolder, AssetsProvider assetsProvider) : base(assetsProvider)
    {
        _lootHolder = lootHolder;
        AssetsProvider = assetsProvider;
    }

    public async Task<Loot> Create(Vector3 position)
    {
        var poolResult = await CreatePoolObject(position, Quaternion.identity);
        Loot loot = poolResult.Result;

        if (poolResult.IsInstantiatedObject == true)
        {
            loot.Init(RewardCount, ExperienceCount, _lootHolder);
        }
        else
        {
            loot.ResetSettings();
        }

        loot.GoToPlayer();

        return loot;
    }
}