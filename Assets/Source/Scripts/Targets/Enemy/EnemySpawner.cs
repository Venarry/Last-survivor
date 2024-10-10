using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner
{
    private readonly WaitForSeconds _waitSpawnDelay = new(GameParamenters.EnemySpawnDelay);
    private readonly List<Enemy> _enemys = new();
    private readonly DayCycle _dayCycleView;
    private readonly EnemyFactory _enemyFactory;
    private readonly LevelsStatisticModel _levelsStatistic;
    private readonly Target _attackTarget;
    private readonly CoroutineProvider _coroutineProvider;
    private Coroutine _activeSpawner;

    public EnemySpawner(
        DayCycle dayCycle,
        EnemyFactory enemyFactory,
        LevelsStatisticModel levelsStatistic,
        Target attackTarget,
        CoroutineProvider coroutineProvider)
    {
        _dayCycleView = dayCycle;
        _enemyFactory = enemyFactory;
        _levelsStatistic = levelsStatistic;
        _attackTarget = attackTarget;
        _coroutineProvider = coroutineProvider;
    }

    public void StartSpawning()
    {
        _dayCycleView.NightCome += OnNightCome;
        _dayCycleView.TimeReset += TryStopSpawner;
    }

    public void DisableSpawning()
    {
        _dayCycleView.NightCome -= OnNightCome;
        _dayCycleView.TimeReset -= TryStopSpawner;
    }

    private void OnNightCome()
    {
        TryStopSpawner();
        _activeSpawner = _coroutineProvider.StartCoroutine(SpawningEnemy());
    }

    private void TryStopSpawner()
    {
        if (_activeSpawner != null)
        {
            _coroutineProvider.StopCoroutine(_activeSpawner);

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
        float health = 3 + _levelsStatistic.TotalLevel + _levelsStatistic.CurrentLevel * 3;
        float damage = 1 + (_levelsStatistic.CurrentLevel * GameParamenters.EnemyDamagePerLevelMultiplier);

        float offsetX = Random.Range(-5f, 5f);
        float offsetZ = Random.Range(-2f, -5f);

        Vector3 spawnOffset = new(offsetX, 0, offsetZ);

        while (true)
        {
            yield return _waitSpawnDelay;

            SpawnEnemy(health, damage, _attackTarget.Position + spawnOffset, Quaternion.identity);
        }
    }

    private async void SpawnEnemy(float health, float damage, Vector3 position, Quaternion rotation)
    {
        Enemy target = await _enemyFactory.Create(_attackTarget, health, damage, position, rotation);
        _enemys.Add(target);
    }
}
