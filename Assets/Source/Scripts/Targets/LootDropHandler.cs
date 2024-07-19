using UnityEngine;

[RequireComponent(typeof(HealthView))]
public class LootDropHandler : MonoBehaviour
{
    [SerializeField] private Rigidbody _lootPrefab;
    [SerializeField] private int _baseLootCount = 1;
    private HealthView _healthView;

    [SerializeField] private float _forceStrength = 1;

    private void Awake()
    {
        _healthView = GetComponent<HealthView>();
    }

    private void OnEnable()
    {
        _healthView.HealthOver += OnHealthOver;
    }

    private void OnDisable()
    {
        _healthView.HealthOver -= OnHealthOver;
    }

    private void OnHealthOver()
    {
        float spawnHeight = 1f;
        Vector3 lootSpawnPosition = transform.position + Vector3.up * spawnHeight;

        for (int i = 0; i < _baseLootCount; i++)
        {
            Rigidbody loot = Instantiate(_lootPrefab, lootSpawnPosition, Quaternion.identity);

            Vector3 forceDirection = new(Random.Range(-1f, 1f), Random.Range(0, 1f), Random.Range(-1f, 1f));
            loot.AddForce(forceDirection * _forceStrength);
        }
    }
}
