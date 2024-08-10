using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private LevelSpawner _levelSpawner;
    [SerializeField] private MapPart _checkpointZonePrefab;
    [SerializeField] private MapPart _betweenLevelsZonePrefab;
    [SerializeField] private float _levelLength;
    [SerializeField] private float _checkpointLength;
    [SerializeField] private float _betweenLevelsZoneLength;

    private Transform _player;
    private LevelsStatisticModel _levelsStatistic;
    private bool _isEnabled;
    private float _currentPosition;

    public void Init(Transform player, LevelsStatisticModel levelsStatistic)
    {
        _player = player;
        _levelsStatistic = levelsStatistic;
    }

    public void StartGenerator()
    {
        _isEnabled = true;
    }

    private void Update()
    {
        if (_isEnabled == false)
            return;

        TrySpawnMap();
    }

    private async void TrySpawnMap()
    {
        if(_player.position.z >= _currentPosition)
        {
            Vector3 spawnPosition = new(0, 0, _currentPosition);

            if (_levelsStatistic.CurrentWave == 0)
            {
                SpawnPart(_checkpointZonePrefab, ref spawnPosition);
            }
            else
            {
                SpawnPart(_betweenLevelsZonePrefab, ref spawnPosition);
            }

            _levelSpawner.ClearObjectsOnMap();
            MapPart mapPart = await _levelSpawner.Spawn(spawnPosition);
            _currentPosition += mapPart.Length;
        }
    }

    private void SpawnPart(MapPart prefab, ref Vector3 spawnPosition)
    {
        MapPart part = Instantiate(prefab, spawnPosition, Quaternion.identity);
        _currentPosition += part.Length;
        spawnPosition = new(0, 0, _currentPosition);
    }
}
