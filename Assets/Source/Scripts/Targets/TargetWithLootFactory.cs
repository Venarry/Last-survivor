using System.Threading.Tasks;
using UnityEngine;

public abstract class TargetWithLootFactory : TargetFactory
{
    private readonly LootFactory _lootFactory;

    public TargetWithLootFactory(
        TargetsProvider targetsProvider,
        AssetsProvider assetsProvider,
        LootFactory lootFactory)
        : base(targetsProvider, assetsProvider)
    {
        _lootFactory = lootFactory;
    }

    public override async Task<Target> Create(float health, Vector3 position, Quaternion rotation)
    {
        Target target = await base.Create(health, position, rotation);

        TargetWithLoot targetWithLoot = target as TargetWithLoot;
        targetWithLoot.InitLootDropHandler(_lootFactory);

        return target;
    }
}
