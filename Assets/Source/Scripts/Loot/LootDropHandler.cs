using UnityEngine;

[RequireComponent(typeof(HealthView))]
public class LootDropHandler : MonoBehaviour
{
    [SerializeField] private Rigidbody _lootPrefab;
    [SerializeField] private int _lootCount = 1;

    private readonly float _forceStrength = 300;
    private HealthView _healthView;
    private LootFactory _lootFactory;

    private void Awake()
    {
        _healthView = GetComponent<HealthView>();
    }

    public void Init(LootFactory lootFactory)
    {
        _lootFactory = lootFactory;
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

        for (int i = 0; i < _lootCount; i++)
        {
            Loot loot = _lootFactory.Create(lootSpawnPosition);

            Vector3 forceDirection = new(Random.Range(-1f, 1f), Random.Range(0, 1f), Random.Range(-1f, 1f));
            loot.AddForce(forceDirection, _forceStrength);
        }
    }
}
