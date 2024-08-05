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

    private Vector3 _startResourcesOffseSpawnPoint = new(-15, 0, 5);
    private Vector3 _endResourcesOffseSpawnPoint = new(15, 0, 25);

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

        int spawnCount = _levelsStatistic.CurrentWave * 5 + 40;

        for (int i = 0; i < spawnCount; i++)
        {
            float spawnPositionX = Random.Range(_startResourcesOffseSpawnPoint.x, _endResourcesOffseSpawnPoint.x);
            float spawnPositionZ = Random.Range(_startResourcesOffseSpawnPoint.z, _endResourcesOffseSpawnPoint.z);

            Vector3 spawnPosition = new(spawnPositionX, 0, spawnPositionZ);
            Quaternion rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);

            if (_levelResourcesSpawnChance.TryGetSpawnAccess(LootType.Diamond) == true)
            {
                await _diamondFactory.Create(3, spawnPosition, rotation);

                continue;
            }

            if(_levelResourcesSpawnChance.TryGetSpawnAccess(LootType.Wood) == true)
            {
                await _woodFactory.Create(3, spawnPosition, rotation);
                continue;
            }

            await _stoneFactory.Create(3, spawnPosition, rotation);
        }
    }
}
