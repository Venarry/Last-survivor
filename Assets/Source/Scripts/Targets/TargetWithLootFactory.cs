using UnityEngine;

public abstract class TargetWithLootFactory : TargetFactory
{
    private readonly LootFactory _lootFactory;

    public TargetWithLootFactory(
        TargetsProvider targetsProvider,
        LootFactory lootFactory)
        : base(targetsProvider)
    {
        _lootFactory = lootFactory;
    }

    public override Target Create(int health, Vector3 position, Quaternion rotation)
    {
        Target target = base.Create(health, position, rotation);

        TargetWithLoot targetWithLoot = target as TargetWithLoot;
        targetWithLoot.InitLootDropHandler(_lootFactory);

        return target;
    }
}
