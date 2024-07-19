using UnityEngine;

[RequireComponent(typeof(LootDropHandler))]
public class Diamond : Target
{
    public override TargetType TargetType => TargetType.Ore;
}