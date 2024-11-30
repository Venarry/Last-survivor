using System;
using UnityEngine;

namespace ObjectPool
{
    public interface IPoolObject<T>
        where T : class
    {
        public event Action<T> LifeCycleEnded;
        public void Respawn(Vector3 spawnPosition, Quaternion rotation);
    }
}