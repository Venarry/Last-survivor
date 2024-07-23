using System;
using UnityEngine;

[RequireComponent(typeof(HealthView))]
public class Target : MonoBehaviour
{
    [SerializeField] private HealthView _healthView;

    public Vector3 Position => transform.position;
    public TargetType TargetType { get; private set; }

    public event Action<Target> HealthOver;

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

        _healthView.HealthOver += OnHealthOver;
    }

    public void TakeDamage(int damage)
    {
        _healthView.TakeDamage(damage);
    }

    private void OnHealthOver()
    {
        HealthOver?.Invoke(this);

        Destroy(gameObject);
    }
}