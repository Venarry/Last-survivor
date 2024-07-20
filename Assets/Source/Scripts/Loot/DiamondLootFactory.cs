using UnityEngine;

public class DiamondLootFactory : LootFactory
{
    protected override Loot Prefab => Resources.Load<Loot>(ResourcesPath.DiamondLoot);
}