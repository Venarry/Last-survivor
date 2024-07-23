using UnityEngine;

public class WoodLootFactory : LootFactory
{
    public WoodLootFactory(ILootHolder lootHolder) : base(lootHolder)
    {
    }

    protected override Loot Prefab => Resources.Load<Loot>(ResourcesPath.WoodLoot);

    protected override LootType LootType => LootType.Wood;
}
