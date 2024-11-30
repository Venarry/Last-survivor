using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Level
{
    public class MapGenerator : MonoBehaviour
    {
        private readonly Queue<MapPart> _mapParts = new ();
        private readonly float _playerPositionOffsetToSpawnPart = -30f;

        [SerializeField] private LevelSpawner _levelSpawner;
        [SerializeField] private MapPart _checkpointZonePrefab;
        [SerializeField] private MapPart _betweenLevelsZonePrefab;
        [SerializeField] private float _levelLength;
        [SerializeField] private float _checkpointLength;
        [SerializeField] private float _betweenLevelsZoneLength;

        private Transform _player;
        private LevelsStatisticModel _levelsStatistic;
        private MapPartsFactory _mapPartsFactory;
        private bool _isEnabled;
        private float _currentPosition = 0;

        public event Action<CheckpointPart> CheckpointZoneSpawned;

        private async void Update()
        {
            if (_isEnabled == false)
            {
                return;
            }

            await TrySpawnMap();
        }

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

        public void StopGenerator()
        {
            _isEnabled = false;
        }

        public async Task ResetLevels()
        {
            await DestroyAllParts();
            await _levelSpawner.RemoveAll();

            _currentPosition = 0;
        }

        private async Task DestroyAllParts()
        {
            foreach (MapPart part in _mapParts)
            {
                Destroy(part.gameObject);

                await Task.Yield();
            }

            _mapParts.Clear();
        }

        private async Task TrySpawnMap()
        {
            if (_player.position.z >= _currentPosition + _playerPositionOffsetToSpawnPart)
            {
                Vector3 spawnPosition = new (0, 0, _currentPosition);
                MapPart part;

                bool applicationIsEnabledInCheckpoint = _levelsStatistic.CurrentLevel == 0 && _mapParts.Count == 0;
                int levelDifficulty = _mapParts.Count == 0 ? _levelsStatistic.CurrentLevel : _levelsStatistic.NextWave;
                int totalLevelDifficulty = _mapParts.Count == 0 ? _levelsStatistic.TotalLevel : _levelsStatistic.TotalLevel + 1;
                bool haveEndLevelTrigger = _mapParts.Count > 0;

                if ((_levelsStatistic.NextWave == 0 && _mapParts.Count > 0) || applicationIsEnabledInCheckpoint)
                {
                    CheckpointPart checkpointPart = await _mapPartsFactory.CreateCheckPointZone(spawnPosition, haveEndLevelTrigger);
                    part = checkpointPart;

                    CheckpointZoneSpawned?.Invoke(checkpointPart);
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
            spawnPosition = new (0, 0, _currentPosition);
        }

        private void TryDeletePassedPart()
        {
            if (_levelSpawner.TryDeletePassedMap())
            {
                MapPart part = _mapParts.Dequeue();
                Destroy(part.gameObject);
            }

            _levelSpawner.TryDeleteLevelObstacle();
        }
    }
}