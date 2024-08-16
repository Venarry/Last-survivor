using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public abstract class ObjectPoolBehaviour<T> where T : MonoBehaviour, IPoolObject<T>
{
    private readonly List<T> _objects = new();
    private readonly AssetsProvider _assetsProvider;

    protected ObjectPoolBehaviour(AssetsProvider assetsProvider)
    {
        _assetsProvider = assetsProvider;
    }

    protected abstract string AssetKey { get; }

    public event Action ActiveObjectsChanged;

    public async Task Load()
    {
        await _assetsProvider.LoadGameObject<T>(AssetKey);
    }

    protected async Task<PoolResult<T>> CreatePoolObject(Vector3 spawnPoint, Quaternion rotation)
    {
        T foundedObject = _objects.FirstOrDefault(c => c.isActiveAndEnabled == false);
        PoolResult<T> poolResult = new();

        if (foundedObject == null)
        {
            T prefab = await _assetsProvider.LoadGameObject<T>(AssetKey);
            foundedObject = UnityEngine.Object.Instantiate(prefab, spawnPoint, rotation);
            _objects.Add(foundedObject);

            poolResult.IsInstantiatedObject = true;
        }
        else
        {
            foundedObject.gameObject.SetActive(true);
            foundedObject.Respawn(spawnPoint, Quaternion.identity);

            poolResult.IsInstantiatedObject = false;
        }

        ActiveObjectsChanged?.Invoke();
        foundedObject.LifeCycleEnded += OnObjectDestroy;

        poolResult.Result = foundedObject;

        return poolResult;
    }

    private void OnObjectDestroy(T target)
    {
        target.LifeCycleEnded -= OnObjectDestroy;
        target.gameObject.SetActive(false);
        ActiveObjectsChanged?.Invoke();
    }
}

public class PoolResult<T> where T : MonoBehaviour, IPoolObject<T>
{
    public T Result { get; set; }
    public bool IsInstantiatedObject { get; set; }
}
