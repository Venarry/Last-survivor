using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Loot : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private int _reward;
    private LootType _lootType;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Init(int reward, LootType lootType)
    {
        _reward = reward;
        _lootType = lootType;
    }

    public void AddForce(Vector3 forceDirection, float forceStrength)
    {
        _rigidbody.AddForce(forceDirection * forceStrength);
    }
}
