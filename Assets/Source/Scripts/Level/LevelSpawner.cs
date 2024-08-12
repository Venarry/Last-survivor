using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    [SerializeField] private MapPart _levelPrefab;

    private WoodFactory _woodFactory;
    private DiamondFactory _diamondFactory;
    private StoneFactory _stoneFactory;
    private LevelResourcesSpawnChance _levelResourcesSpawnChance;
    private LevelsStatisticModel _levelsStatistic;
    private readonly Queue<KeyValuePair<MapPart, List<Target>>> _targets = new();

    private Vector3 _startResourcesOffseSpawnPoint = new(-15, 0, 10);
    private Vector3 _endResourcesOffseSpawnPoint = new(15, 0, 35);

    public void Init(
        WoodFactory woodFactory,
        DiamondFactory diamondFactory,
        StoneFactory stoneFactory,
        LevelResourcesSpawnChance levelResourcesSpawnChance,
        LevelsStatisticModel levelsStatistic)
    {
        _woodFactory = woodFactory;
        _diamondFactory = diamondFactory;
        _stoneFactory = stoneFactory;
        _levelResourcesSpawnChance = levelResourcesSpawnChance;
        _levelsStatistic = levelsStatistic;
    }

    public async Task<MapPart> Spawn(Vector3 position)
    {
        MapPart map = Instantiate(_levelPrefab, position, Quaternion.identity);
        map.GetComponent<EndlLevelTrigger>().Init(_levelsStatistic);

        List<Vector3> spawnPoints = new();
        int spawnCount = 100 + _levelsStatistic.CurrentWave * 5;

        int rowsCount = (int)Mathf.Floor(Mathf.Sqrt(spawnCount));
        int colsCount = (int)Mathf.Floor(spawnCount / rowsCount);

        float cellOfssetX = (_endResourcesOffseSpawnPoint.x - _startResourcesOffseSpawnPoint.x) / (colsCount - 1); // благодаря -1 мы получаем расчет для спавна на один элемент меньше, а потом в цикле в 0 координате доспавливаем этот элемент
        float cellOfssetZ = (_endResourcesOffseSpawnPoint.z - _startResourcesOffseSpawnPoint.z) / (rowsCount - 1); // потому что в противном случае или первый или последний стобец\строка отстутствуют

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

        int healthPerTotalWave = _levelsStatistic.TotalWave + 1;
        int healthPerCurrentWave;

        if (_levelsStatistic.CurrentWave + 1 != LevelsStatisticModel.LevelForCheckpoint)
        {
            healthPerCurrentWave = (_levelsStatistic.CurrentWave + 1) * 2;
        }
        else
        {
            healthPerCurrentWave = 2;
        }

        int health = 1 + healthPerTotalWave + healthPerCurrentWave;
        List<Target> targetsInLevel = new();
        _targets.Enqueue(new(map, targetsInLevel));

        foreach (Vector3 spawnPosition in spawnPoints)
        {
            float randomSpawnOffset = 2f;

            float offsetX = Random.Range(-randomSpawnOffset, randomSpawnOffset);
            float offsetZ = Random.Range(randomSpawnOffset, randomSpawnOffset);

            Vector3 targetPosition = spawnPosition + new Vector3(offsetX, 0, offsetZ) + position;
            Quaternion rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);

            if (_levelResourcesSpawnChance.TryGetSpawnAccess(LootType.Diamond) == true)
            {
                //_targets.Add(map, await _diamondFactory.Create(health, targetPosition, rotation));
                targetsInLevel.Add(await _diamondFactory.Create(health, targetPosition, rotation));
                continue;
            }

            if (_levelResourcesSpawnChance.TryGetSpawnAccess(LootType.Wood) == true)
            {
                //_targets.Add(map, await _woodFactory.Create(health, targetPosition, rotation));
                targetsInLevel.Add(await _woodFactory.Create(health, targetPosition, rotation));
                continue;
            }

            targetsInLevel.Add(await _stoneFactory.Create(health, targetPosition, rotation));
        }

        return map;
    }

    public void TryDeletePassedMap()
    {
        if (_targets.Count <= GameParamenters.SpawnedMapBufferCount)
            return;

        KeyValuePair<MapPart, List<Target>> zeroMap = _targets.Dequeue();

        foreach (Target target in zeroMap.Value)
        {
            target.PlaceInPool();
        }

        Destroy(zeroMap.Key.gameObject);
    }
}
