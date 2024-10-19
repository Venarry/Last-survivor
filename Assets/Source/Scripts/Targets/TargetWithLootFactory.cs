using UnityEngine;

public abstract class TargetWithLootFactory : TargetFactory
{
    private readonly LevelsStatisticModel _levelsStatisticModel;
    private readonly LootFactory _lootFactory;

    public TargetWithLootFactory(
        LevelsStatisticModel levelsStatisticModel,
        TargetsProvider<Target> targetsProvider,
        AssetsProvider assetsProvider,
        AudioSource audioSource,
        LootFactory lootFactory)
        : base(targetsProvider, assetsProvider, audioSource)
    {
        _levelsStatisticModel = levelsStatisticModel;
        _lootFactory = lootFactory;
    }

    protected override void OnCreated(Target target, HealthModel healthModel)
    {
        TargetWithLoot targetWithLoot = target as TargetWithLoot;
        targetWithLoot.InitLootDropHandler(healthModel, _lootFactory, _levelsStatisticModel);
    }
}
