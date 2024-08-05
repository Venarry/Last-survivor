using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _levelPrefab;

    private EnemyFactory _enemyFactory;
    private WoodFactory _woodFactory;
    private DiamondFactory _diamondFactory;
    private StoneFactory _stoneFactory;
    private LevelResourcesSpawnChance _levelResourcesSpawnChance;
    private LevelsStatistic _levelsStatistic;

    private Vector3 _startResourcesOffseSpawnPoint = new(-15, 0, 10);
    private Vector3 _endResourcesOffseSpawnPoint = new(15, 0, 40);

    public void Init(
        EnemyFactory enemyFactory,
        WoodFactory woodFactory,
        DiamondFactory diamondFactory,
        StoneFactory stoneFactory,
        LevelResourcesSpawnChance levelResourcesSpawnChance,
        LevelsStatistic levelsStatistic)
    {
        _enemyFactory = enemyFactory;
        _woodFactory = woodFactory;
        _diamondFactory = diamondFactory;
        _stoneFactory = stoneFactory;
        _levelResourcesSpawnChance = levelResourcesSpawnChance;
        _levelsStatistic = levelsStatistic;
    }

    public async void Spawn(Vector3 position)
    {
        Instantiate(_levelPrefab, position, Quaternion.identity);

        List<Vector3> spawnPoints = new();
        int spawnCount = 60 + _levelsStatistic.CurrentWave * 5;

        int rowsMultiplier = 7;
        int rowsCount = spawnCount / rowsMultiplier;
        int colsCount = spawnCount / rowsMultiplier;

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

        int health = 3 + _levelsStatistic.TotalWave + _levelsStatistic.CurrentWave * 2;
        Debug.Log(health);
        Debug.Log(_levelsStatistic.TotalWave);
        Debug.Log(_levelsStatistic.CurrentWave);

        foreach (Vector3 spawnPosition in spawnPoints)
        {
            float randomSpawnOffset = 2f;

            float offsetX = Random.Range(-randomSpawnOffset, randomSpawnOffset);
            float offsetZ = Random.Range(randomSpawnOffset, randomSpawnOffset);

            Vector3 targetPosition = spawnPosition + new Vector3(offsetX, 0, offsetZ);
            Quaternion rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);


            if (_levelResourcesSpawnChance.TryGetSpawnAccess(LootType.Diamond) == true)
            {
                await _diamondFactory.Create(health, targetPosition, rotation);
                continue;
            }

            if (_levelResourcesSpawnChance.TryGetSpawnAccess(LootType.Wood) == true)
            {
                await _woodFactory.Create(health, targetPosition, rotation);
                continue;
            }

            await _stoneFactory.Create(health, targetPosition, rotation);
        }
    }
}
