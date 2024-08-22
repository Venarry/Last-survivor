using System;
using UnityEngine;

[RequireComponent(typeof(HealthView))]
public class Target : MonoBehaviour, IPoolObject<Target>
{
    [SerializeField] private bool _isFriendly = false;
    private HealthView _healthView;
    private HealthModel _healthModel;

    public Vector3 Position => transform.position;
    public TargetType TargetType { get; private set; }
    public bool IsFriendly => _isFriendly;

    public event Action<Target> LifeCycleEnded;

    private void Awake()
    {
        _healthView = GetComponent<HealthView>();

        OnAwake();
    }

    protected virtual void OnAwake()
    {
    }

    public void Init(TargetType targetType, HealthModel healthModel)
    {
        TargetType = targetType;
        _healthModel = healthModel;
        _healthView.Init(healthModel);

        _healthModel.HealthOver += OnHealthOver;
    }

    private void OnDestroy()
    {
        _healthModel.HealthOver -= OnHealthOver;
    }

    public void TakeDamage(float damage)
    {
        _healthView.TakeDamage(damage);
    }

    public void PlaceInPool()
    {
        LifeCycleEnded?.Invoke(this);
    }

    private void OnHealthOver()
    {
        LifeCycleEnded?.Invoke(this);
    }

    public void Respawn(Vector3 spawnPosition, Quaternion rotation)
    {
        transform.position = spawnPosition;
        transform.rotation = rotation;
    }

    public void ResetSettings(float health)
    {
        _healthModel.SetMaxHealth(health);
        _healthModel.Restore();
    }
}