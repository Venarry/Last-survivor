using UnityEngine;

namespace ObjectPool
{
    public class PoolSpawnResult<T>
        where T : MonoBehaviour, IPoolObject<T>
    {
        public T Result { get; set; }
        public bool IsInstantiatedObject { get; set; }
    }
}