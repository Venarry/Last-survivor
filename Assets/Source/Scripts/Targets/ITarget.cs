using UnityEngine;

public interface ITarget
{
    public Vector3 Position { get; }
    public TargetType TargetType { get; }

    public void TakeDamage();
}