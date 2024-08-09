using System;
using UnityEngine;

public interface IPoolObject<T> where T : class
{
    public event Action<T> LifeCycleEnded;
    public void Respawn(Vector3 spawnPosition, Quaternion rotation);
}
