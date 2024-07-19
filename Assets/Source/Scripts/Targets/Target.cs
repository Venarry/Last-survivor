using System;
using UnityEngine;

[RequireComponent(typeof(HealthView))]
public abstract class Target : MonoBehaviour
{
    [SerializeField] private HealthView _healthView;

    public Vector3 Position => transform.position;
    public abstract TargetType TargetType { get; }

    public event Action<Target> HealthOver;

    private void OnEnable()
    {
        _healthView.HealthOver += OnHealthOver;
    }

    private void OnDisable()
    {
        _healthView.HealthOver -= OnHealthOver;
    }

    public void SetHealthModel(HealthModel healthModel)
    {
        _healthView.Init(healthModel);
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