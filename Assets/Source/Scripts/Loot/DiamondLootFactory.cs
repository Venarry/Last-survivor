using UnityEngine;

public class DiamondLootFactory : LootFactory
{
    public DiamondLootFactory(ILootHolder lootHolder) : base(lootHolder)
    {
    }

    protected override Loot Prefab => Resources.Load<Loot>(ResourcesPath.DiamondLoot);

    protected override LootType LootType => LootType.Diamond;
}