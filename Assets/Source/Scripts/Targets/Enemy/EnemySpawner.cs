using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private const float SpawnDelay = 2f;

    [SerializeField] private DayCycleView _dayCycleView;

    private readonly WaitForSeconds _waitSpawnDelay = new(SpawnDelay);
    private EnemyFactory _enemyFactory;
    private LevelsStatisticModel _levelsStatistic;
    private Target _attackTarget;
    private Coroutine _activeSpawner;

    public void Init(EnemyFactory enemyFactory, LevelsStatisticModel levelsStatistic, Target attackTarget)
    {
        _enemyFactory = enemyFactory;
        _levelsStatistic = levelsStatistic;
        _attackTarget = attackTarget;
    }

    private void OnEnable()
    {
        _dayCycleView.NightStarted += OnNightStart;
        _dayCycleView.NightEnded += TryStopSpawner;
    }
    
    private void OnNightStart()
    {
        TryStopSpawner();
        _activeSpawner = StartCoroutine(SpawningEnemy());
    }

    private void TryStopSpawner()
    {
        if (_activeSpawner != null)
        {
            StopCoroutine(_activeSpawner);
            _activeSpawner = null;
        }
    }

    private IEnumerator SpawningEnemy()
    {
        float health = 3 + _levelsStatistic.TotalWave;
        float damage = 1 + _levelsStatistic.TotalWave;

        float offsetX = Random.Range(-5f, 5f);
        float offsetZ = Random.Range(2f, 5f);

        Vector3 spawnOffset = new(offsetX, 0, offsetZ);

        while (true)
        {
            _enemyFactory.Create(_attackTarget, health, damage, _attackTarget.Position + spawnOffset, Quaternion.identity);

            yield return _waitSpawnDelay;
        }
    }
}
