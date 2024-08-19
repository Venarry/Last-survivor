using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameTimeScaler
{
    private readonly Dictionary<string, float> _timeScales = new();
    private string _currentKey;
    private float _defaultTimeScale = 1f;

    public void Add(string key, float timeScale)
    {
        if (_timeScales.ContainsKey(key) == true)
            return;

        _timeScales[key] = timeScale;

        RefreshTime();
    }

    public void Remove(string key)
    {
        if (_timeScales.ContainsKey(key) == false)
            return;

        _timeScales.Remove(key);

        if (_currentKey == key)
        {
            RefreshTime();
        }
    }

    public void RemoveAll()
    {
        Time.timeScale = _defaultTimeScale;
        _timeScales.Clear();
    }

    private void RefreshTime()
    {
        if (_timeScales.Count == 0)
        {
            Time.timeScale = _defaultTimeScale;
            return;
        }

        KeyValuePair<string, float> time = _timeScales.OrderBy(c => c.Value).ToArray().First();

        Time.timeScale = time.Value;
        _currentKey = time.Key;
    }
}
