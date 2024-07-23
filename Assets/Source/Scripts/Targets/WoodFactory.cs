using UnityEngine;

public class WoodFactory : TargetWithLootFactory
{
    public WoodFactory(
        TargetsProvider targetsProvider,
        WoodLootFactory lootFactory) 
        : base(targetsProvider, lootFactory)
    {
    }

    protected override Target Prefab => Resources.Load<Target>(ResourcesPath.Wood);
    protected override TargetType TargetType => TargetType.Wood;
}
