using System;
using UnityEngine;

public interface IPoolObject<T> where T : class
{
    public event Action<T> HealthOver;
    public void Respawn(Vector3 spawnPosition, Quaternion rotation);
}
