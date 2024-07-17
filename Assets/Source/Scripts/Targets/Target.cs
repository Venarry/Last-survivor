using UnityEngine;

[RequireComponent(typeof(HealthView))]
public abstract class Target : MonoBehaviour
{
    [SerializeField] private HealthView _healthView;

    public Vector3 Position { get; }
    public abstract TargetType TargetType { get; }

    public void TakeDamage(int damage)
    {
        _healthView.TakeDamage(damage);
    }
}