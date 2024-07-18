using UnityEngine;

[RequireComponent(typeof(HealthView))]
public abstract class Target : MonoBehaviour
{
    [SerializeField] private HealthView _healthView;

    public Vector3 Position => transform.position;
    public abstract TargetType TargetType { get; }

    public void TakeDamage(int damage)
    {
        Debug.Log($"Take {damage} damage");
        //_healthView.TakeDamage(damage);
    }
}