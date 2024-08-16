using System;
using UnityEngine;

[RequireComponent(typeof(HealthView))]
[RequireComponent(typeof(TargetHealthOverReaction))]
public class Target : MonoBehaviour, IPoolObject<Target>
{
    [SerializeField] private bool _isFriendly = false;
    private HealthView _healthView;
    private TargetHealthOverReaction _healthOverReaction;

    public Vector3 Position => transform.position;
    public TargetType TargetType { get; private set; }
    public bool IsFriendly => _isFriendly;

    public event Action<Target> LifeCycleEnded;

    private void Awake()
    {
        _healthView = GetComponent<HealthView>();
        _healthOverReaction = GetComponent<TargetHealthOverReaction>();

        OnAwake();
    }

    protected virtual void OnAwake()
    {
    }

    private void OnEnable()
    {
        _healthView.HealthOver += OnHealthOver;
    }

    private void OnDisable()
    {
        _healthView.HealthOver -= OnHealthOver;
    }

    public void Init(TargetType targetType, HealthModel healthModel)
    {
        TargetType = targetType;
        _healthView.Init(healthModel);
        _healthOverReaction.Init(healthModel);

        _healthView.HealthOver += OnHealthOver;
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
        _healthView.SetMaxHealth(health);
        _healthView.Restore();
    }
}