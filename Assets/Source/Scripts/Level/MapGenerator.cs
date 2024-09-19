using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private LevelSpawner _levelSpawner;
    [SerializeField] private MapPart _checkpointZonePrefab;
    [SerializeField] private MapPart _betweenLevelsZonePrefab;
    [SerializeField] private float _levelLength;
    [SerializeField] private float _checkpointLength;
    [SerializeField] private float _betweenLevelsZoneLength;

    private readonly Queue<MapPart> _mapParts = new();
    private readonly float _playerPositionOffsetToSpawnPart = -30f;

    private Transform _player;
    private LevelsStatisticModel _levelsStatistic;
    private MapPartsFactory _mapPartsFactory;
    private bool _isEnabled;
    private float _currentPosition;

    public void Init(
        Transform player,
        LevelsStatisticModel levelsStatistic,
        MapPartsFactory mapPartsFactory)
    {
        _player = player;
        _levelsStatistic = levelsStatistic;
        _mapPartsFactory = mapPartsFactory;
    }

    private async void Update()
    {
        if (_isEnabled == false)
            return;

        await TrySpawnMap();
    }

    public void StartGenerator()
    {
        _isEnabled = true;
    }

    public void ResetLevels()
    {
        foreach (MapPart part in _mapParts)
        {
            Destroy(part.gameObject);
        }

        _mapParts.Clear();
        _levelSpawner.RemoveAll();
        _currentPosition = 0;
    }

    private async Task TrySpawnMap()
    {
        if(_player.position.z >= _currentPosition + _playerPositionOffsetToSpawnPart)
        {
            Vector3 spawnPosition = new(0, 0, _currentPosition);
            MapPart part;

            bool startInCheckpoint = _levelsStatistic.CurrentLevel == 0 && _mapParts.Count == 0;
            int levelDifficulty = _mapParts.Count == 0 ? _levelsStatistic.CurrentLevel : _levelsStatistic.NextWave;
            int totalLevelDifficulty = _mapParts.Count == 0 ? _levelsStatistic.TotalLevel : _levelsStatistic.TotalLevel + 1;
            bool haveEndLevelTrigger = _mapParts.Count > 0;

            if (_levelsStatistic.NextWave == 0 || startInCheckpoint)
            {
                part = await _mapPartsFactory.CreateCheckPointZone(spawnPosition, haveEndLevelTrigger);
            }
            else
            {
                part = await _mapPartsFactory.CreateBetweenLevelZone(spawnPosition, haveEndLevelTrigger);
            }

            RegisterPart(part, ref spawnPosition);

            MapPart levelZone = await _levelSpawner.Spawn(spawnPosition, levelDifficulty, totalLevelDifficulty);
            _currentPosition += levelZone.Length;

            TryDeletePassedPart();
        }
    }

    private void RegisterPart(MapPart mapPart, ref Vector3 spawnPosition)
    {
        _currentPosition += mapPart.Length;
        _mapParts.Enqueue(mapPart);
        spawnPosition = new(0, 0, _currentPosition);
    }

    private void TryDeletePassedPart()
    {
        if (_mapParts.Count <= GameParamenters.SpawnedMapBufferCount)
            return;

        _levelSpawner.TryDeletePassedMap();
        MapPart part = _mapParts.Dequeue();
        Destroy(part.gameObject);
    }
}