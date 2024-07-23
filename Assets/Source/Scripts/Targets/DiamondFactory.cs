using UnityEngine;

public class DiamondFactory : TargetWithLootFactory
{
    private readonly DiamondLootFactory _lootFactory;

    public DiamondFactory(
        TargetsProvider targetsProvider,
        DiamondLootFactory lootFactory) 
        : base(targetsProvider, lootFactory)
    {
        _lootFactory = lootFactory;
    }

    protected override Target Prefab => Resources.Load<Target>(ResourcesPath.Diamond);
    protected override TargetType TargetType => TargetType.Ore;
}