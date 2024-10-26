using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameTimeScaler
{
    private static readonly Dictionary<string, float> _timeScales = new();
    private static readonly float _defaultTimeScale = 1f;
    private static string _currentKey;

    public static void Add(string key, float timeScale)
    {
        if (_timeScales.ContainsKey(key) == true)
            return;

        _timeScales[key] = timeScale;

        RefreshTime();
    }

    public static void Remove(string key)
    {
        if (_timeScales.ContainsKey(key) == false)
            return;

        _timeScales.Remove(key);

        if (_currentKey == key)
        {
            RefreshTime();
        }
    }

    public static void RemoveAll()
    {
        Time.timeScale = _defaultTimeScale;
        _timeScales.Clear();
    }

    private static void RefreshTime()
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
