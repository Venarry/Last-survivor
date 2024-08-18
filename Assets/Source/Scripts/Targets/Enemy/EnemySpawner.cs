using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private const float SpawnDelay = 2f;

    [SerializeField] private DayCycle _dayCycleView;

    private readonly WaitForSeconds _waitSpawnDelay = new(SpawnDelay);
    private EnemyFactory _enemyFactory;
    private LevelsStatisticModel _levelsStatistic;
    private Target _attackTarget;
    private Coroutine _activeSpawner;
    private readonly List<Enemy> _enemys = new();

    public void Init(EnemyFactory enemyFactory, LevelsStatisticModel levelsStatistic, Target attackTarget)
    {
        _enemyFactory = enemyFactory;
        _levelsStatistic = levelsStatistic;
        _attackTarget = attackTarget;
    }

    private void OnEnable()
    {
        _dayCycleView.NightCome += OnNightCome;
        _dayCycleView.TimeReset += TryStopSpawner;
    }
    
    private void OnNightCome()
    {
        TryStopSpawner();
        _activeSpawner = StartCoroutine(SpawningEnemy());
    }

    private void TryStopSpawner()
    {
        if (_activeSpawner != null)
        {
            StopCoroutine(_activeSpawner);

            foreach (Enemy enemy in _enemys)
            {
                enemy.PlaceInPool();
            }

            _enemys.Clear();
            _activeSpawner = null;
        }
    }

    private IEnumerator SpawningEnemy()
    {
        float health = 3 + _levelsStatistic.TotalWave;
        float damage = 1 + (_levelsStatistic.TotalWave * GameParamenters.EnemyDamageMultiplier);

        float offsetX = Random.Range(-5f, 5f);
        float offsetZ = Random.Range(-2f, -5f);

        Vector3 spawnOffset = new(offsetX, 0, offsetZ);

        while (true)
        {
            SpawnEnemy(health, damage, _attackTarget.Position + spawnOffset, Quaternion.identity);

            yield return _waitSpawnDelay;
        }
    }

    private async void SpawnEnemy(float health, float damage, Vector3 position, Quaternion rotation)
    {
        Enemy target = await _enemyFactory.Create(_attackTarget, health, damage, position, rotation);
        _enemys.Add(target);
    }
}
