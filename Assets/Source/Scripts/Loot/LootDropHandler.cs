using UnityEngine;

[RequireComponent(typeof(HealthView))]
public class LootDropHandler : MonoBehaviour
{
    [SerializeField] private int _lootCount = 1;

    private readonly float _forceStrength = 300;
    private HealthModel _healthModel;
    private LootFactory _lootFactory;
    private LevelsStatisticModel _levelsStatisticModel;

    public void Init(HealthModel healthModel, LootFactory lootFactory, LevelsStatisticModel levelsStatisticModel)
    {
        _healthModel = healthModel;
        _lootFactory = lootFactory;
        _levelsStatisticModel = levelsStatisticModel;
        _healthModel.HealthOver += OnHealthOver;
    }

    private void OnDestroy()
    {
        _healthModel.HealthOver -= OnHealthOver;
    }

    private async void OnHealthOver()
    {
        float spawnHeight = 1f;
        Vector3 lootSpawnPosition = transform.position + Vector3.up * spawnHeight;
        int rewardMultiplier = _levelsStatisticModel.TotalWave + 1;

        for (int i = 0; i < _lootCount; i++)
        {
            Loot loot = await _lootFactory.Create(lootSpawnPosition, rewardMultiplier);

            Vector3 forceDirection = new(Random.Range(-1f, 1f), Random.Range(0, 1f), Random.Range(-1f, 1f));
            loot.AddForce(forceDirection, _forceStrength);
        }
    }
}
