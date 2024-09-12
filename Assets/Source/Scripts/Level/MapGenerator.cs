using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor.Experimental.GraphView;
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
    private Transform _player;
    private LevelsStatisticModel _levelsStatistic;
    private MapPartsFactory _mapPartsFactory;
    private bool _isEnabled;
    private float _currentPosition;
    private float _spawnOffsetPosition = -30f;

    public void Init(
        Transform player,
        LevelsStatisticModel levelsStatistic,
        MapPartsFactory mapPartsFactory)
    {
        _player = player;
        _levelsStatistic = levelsStatistic;
        _mapPartsFactory = mapPartsFactory;
    }

    public void StartGenerator()
    {
        _isEnabled = true;
    }

    private async void Update()
    {
        if (_isEnabled == false)
            return;

        await TrySpawnMap();
    }

    private async Task TrySpawnMap()
    {
        if(_player.position.z >= _currentPosition + _spawnOffsetPosition)
        {
            Vector3 spawnPosition = new(0, 0, _currentPosition);
            MapPart part;
            //(_levelsStatistic.CurrentLevel + 1) % GameParamenters.LevelsForCheckpoint == 0

            bool startInCheckpoint = _levelsStatistic.CurrentLevel == 0 && _mapParts.Count == 0;

            if (_levelsStatistic.NextWave == 0 || startInCheckpoint)
            {
                bool haveEndLevelTrigger = _mapParts.Count > 0;
                part = await _mapPartsFactory.CreateCheckPointZone(spawnPosition, haveEndLevelTrigger);
            }
            else
            {
                part = await _mapPartsFactory.CreateBetweenLevelZone(spawnPosition);
            }

            RegisterPart(part, ref spawnPosition);

            MapPart levelZone = await _levelSpawner.Spawn(spawnPosition);
            _currentPosition += levelZone.Length;

            _levelSpawner.TryDeletePassedMap();
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

        MapPart part = _mapParts.Dequeue();
        Destroy(part.gameObject);
    }
}