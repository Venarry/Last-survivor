using UnityEngine;

[RequireComponent(typeof(HealthView))]
public abstract class Target : MonoBehaviour
{
    [SerializeField] private HealthView _healthView;

    public Vector3 Position => transform.position;
    public abstract TargetType TargetType { get; }

    public void SetHealthModel(HealthModel healthModel)
    {
        _healthView.Init(healthModel);
    }

    public void TakeDamage(int damage)
    {
        _healthView.TakeDamage(damage);
    }
}