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
    private LevelResourcesSpawnChance _levelResourcesSpawnChance;
    private LevelsStatisticModel _levelsStatistic;
    private readonly Queue<KeyValuePair<MapPart, List<Target>>> _targetsOnMap = new();

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
        int spawnCount = 100;

        if (_targetsOnMap.Count != 0)
        {
            spawnCount += _levelsStatistic.NextWave * 5;
        }

        Debug.Log(spawnCount);

        int rowsCount = (int)Mathf.Floor(Mathf.Sqrt(spawnCount));
        int colsCount = (int)Mathf.Floor(spawnCount / rowsCount);

        float cellOfssetX = (_endResourcesOffseSpawnPoint.x - _startResourcesOffseSpawnPoint.x) / (colsCount - 1); // ��������� -1 �� �������� ������ ��� ������ �� ���� ������� ������, � ����� � ����� � 0 ���������� ������������ ���� �������
        float cellOfssetZ = (_endResourcesOffseSpawnPoint.z - _startResourcesOffseSpawnPoint.z) / (rowsCount - 1); // ������ ��� � ��������� ������ ��� ������ ��� ��������� ������\������ ������������

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

        if (_levelsStatistic.NextWave != 0)
        {
            healthPerCurrentWave = _levelsStatistic.NextWave * 2;
        }
        else
        {
            healthPerCurrentWave = 2;
        }

        int health = 1 + healthPerTotalWave + healthPerCurrentWave;
        List<Target> targetsInLevel = new();
        _targetsOnMap.Enqueue(new(map, targetsInLevel));

        foreach (Vector3 spawnPosition in spawnPoints)
        {
            float randomSpawnOffset = 1.2f;

            float offsetX = Random.Range(-randomSpawnOffset, randomSpawnOffset);
            float offsetZ = Random.Range(randomSpawnOffset, randomSpawnOffset);

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

    private async Task SpawnObstacle(TargetFactory targetFactory, int health, Vector3 position, Quaternion rotation, List<Target> pool)
    {
        Target obstacle = await targetFactory.Create(health, position, rotation);
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
