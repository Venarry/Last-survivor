using System;
using UnityEngine;

[RequireComponent(typeof(HealthView))]
[RequireComponent(typeof(TargetHealthOverReaction))]
public class Target : MonoBehaviour
{
    [SerializeField] private bool _isFriendly = false;
    private HealthView _healthView;
    private TargetHealthOverReaction _healthOverReaction;

    public Vector3 Position => transform.position;
    public TargetType TargetType { get; private set; }
    public bool IsFriendly => _isFriendly;

    public event Action<Target> HealthOver;

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

    public void TakeDamage(int damage)
    {
        _healthView.TakeDamage(damage);
    }

    private void OnHealthOver()
    {
        HealthOver?.Invoke(this);
    }
}