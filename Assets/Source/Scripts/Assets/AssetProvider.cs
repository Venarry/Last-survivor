using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using static UnityEditor.Progress;

public class AssetProvider
{
    private readonly Dictionary<string, AsyncOperationHandle> _handlers = new();
    private readonly Dictionary<string, AsyncOperationHandle> _loadingHandlers = new();

    public async Task<T> Load<T>(string key) where T : class
    {
        if(_handlers.ContainsKey(key) == true)
        {
            return _handlers[key].Result as T;
        }

        if(_loadingHandlers.ContainsKey(key) == true)
        {
            return await _loadingHandlers[key].Task as T;
        }

        AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(key);

        if(_loadingHandlers.ContainsKey(key) == false)
        {
            _loadingHandlers.Add(key, handle);
        }

        T task = await handle.Task;

        if (_handlers.ContainsKey(key) == false)
        {
            _handlers.Add(key, handle);
        }

        return task;
    }

    public void Clear()
    {
        foreach (KeyValuePair<string, AsyncOperationHandle> item in _handlers)
        {
            Addressables.Release(item.Value);
        }

        _handlers.Clear();
        _loadingHandlers.Clear();
    }

    public void Clear(string key)
    {
        if (_handlers.ContainsKey(key) == false)
            return;

        Addressables.Release(_handlers[key]);
        _handlers.Remove(key);
        _loadingHandlers.Remove(key);
    }
}
