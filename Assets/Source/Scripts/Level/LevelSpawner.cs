using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    [SerializeField] private MapPart _levelPrefab;

    private WoodFactory _woodFactory;
    private DiamondFactory _diamondFactory;
    private StoneFactory _stoneFactory;
    private MapPartsFactory _mapPartsFactory;
    private LevelResourcesSpawnChance _levelResourcesSpawnChance;
    private LevelsStatisticModel _levelsStatistic;
    private readonly Queue<KeyValuePair<MapPart, List<Target>>> _targetsOnMap = new();

    private Vector3 _startResourcesOffseSpawnPoint = new(-20, 0, 10);
    private Vector3 _endResourcesOffseSpawnPoint = new(20, 0, 50);

    public void Init(
        WoodFactory woodFactory,
        DiamondFactory diamondFactory,
        StoneFactory stoneFactory,
        MapPartsFactory mapPartsFactory,
        LevelResourcesSpawnChance levelResourcesSpawnChance,
        LevelsStatisticModel levelsStatistic)
    {
        _woodFactory = woodFactory;
        _diamondFactory = diamondFactory;
        _stoneFactory = stoneFactory;
        _mapPartsFactory = mapPartsFactory;
        _levelResourcesSpawnChance = levelResourcesSpawnChance;
        _levelsStatistic = levelsStatistic;
    }

    public async Task<MapPart> Spawn(Vector3 position)
    {
        MapPart map = await _mapPartsFactory.CreateLevelZone(position);

        List<Vector3> spawnPoints = new();

        float mapSizeX = _endResourcesOffseSpawnPoint.x - _startResourcesOffseSpawnPoint.x;
        float mapSizeZ = _endResourcesOffseSpawnPoint.z - _startResourcesOffseSpawnPoint.z;

        int baseSpawnCount = (int)Mathf.Floor(mapSizeX * mapSizeZ / 8);

        if (_targetsOnMap.Count != 0)
        {
            int spawnCountByWave = 7;
            baseSpawnCount += _levelsStatistic.NextWave * spawnCountByWave;
        }

        int rowsCount = (int)Mathf.Floor(Mathf.Sqrt(baseSpawnCount));
        int colsCount = (int)Mathf.Floor(baseSpawnCount / rowsCount);

        float cellOfssetX = mapSizeX / (colsCount - 1); // благодаря -1 мы получаем расчет для спавна на один элемент меньше, а потом в цикле в 0 координате доспавливаем этот элемент
        float cellOfssetZ = mapSizeZ / (rowsCount - 1); // потому что в противном случае или первый или последний стобец\строка отстутствуют

        for (int i = 0; i < rowsCount; i++)
        {
            for (int j = 0; j < colsCount; j++)
            {
                spawnPoints.Add(new Vector3(
                    j * cellOfssetX,
                    0,
                    i * cellOfssetZ) + _startResourcesOffseSpawnPoint);
            }
        }

        float healthPerTotalWave = _levelsStatistic.TotalLevel + 1;
        float healthPerCurrentWave;
        float healthPerCurrentWaveMultiplier = 3;

        if (_levelsStatistic.NextWave != 0)
        {
            healthPerCurrentWave = _levelsStatistic.NextWave * healthPerCurrentWaveMultiplier;
        }
        else
        {
            healthPerCurrentWave = healthPerCurrentWaveMultiplier / 2;
        }

        float health = 1 + healthPerTotalWave + healthPerCurrentWave;
        List<Target> targetsInLevel = new();
        _targetsOnMap.Enqueue(new(map, targetsInLevel));

        foreach (Vector3 spawnPosition in spawnPoints)
        {
            float randomSpawnOffset = 0.8f;

            float offsetX = Random.Range(-randomSpawnOffset, randomSpawnOffset);
            float offsetZ = Random.Range(-randomSpawnOffset, randomSpawnOffset);

            Vector3 targetPosition = spawnPosition + new Vector3(offsetX, 0, offsetZ) + position;
            Quaternion rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);

            if (_levelResourcesSpawnChance.TryGetSpawnAccess(LootType.Diamond) == true)
            {
                await SpawnObstacle(_diamondFactory, health, targetPosition, rotation, targetsInLevel);
                continue;
            }

            if (_levelResourcesSpawnChance.TryGetSpawnAccess(LootType.Wood) == true)
            {
                await SpawnObstacle(_woodFactory, health, targetPosition, rotation, targetsInLevel);
                continue;
            }

            await SpawnObstacle(_stoneFactory, health, targetPosition, rotation, targetsInLevel);
        }

        return map;
    }

    public void TryDeletePassedMap()
    {
        if (_targetsOnMap.Count <= GameParamenters.SpawnedMapBufferCount)
            return;

        KeyValuePair<MapPart, List<Target>> zeroMap = _targetsOnMap.Dequeue();

        foreach (Target target in zeroMap.Value)
        {
            target.PlaceInPool();
        }

        Destroy(zeroMap.Key.gameObject);
    }

    private async Task SpawnObstacle(TargetFactory targetFactory, float health, Vector3 position, Quaternion rotation, List<Target> pool)
    {
        Target obstacle = (await targetFactory.Create(health, position, rotation)).Result;
        pool.Add(obstacle);

        obstacle.LifeCycleEnded += OnTargetLifeCycleEnd;
    }

    private void OnTargetLifeCycleEnd(Target removedTarget)
    {
        removedTarget.LifeCycleEnded -= OnTargetLifeCycleEnd;

        List<Target> listWithRemovedTarget = new();

        foreach (KeyValuePair<MapPart, List<Target>> targets in _targetsOnMap)
        {
            foreach (Target target in targets.Value)
            {
                if(removedTarget == target)
                {
                    listWithRemovedTarget = targets.Value;
                }
            }
        }

        listWithRemovedTarget.Remove(removedTarget);
    }
}
