using UnityEngine;

[RequireComponent(typeof(ThirdPersonRotation))]
public class PlayerAttackHandler : MonoBehaviour
{
    private TargetsProvider _targetsProvider;
    private ThirdPersonRotation _thirdPersonRotation;
    private float _attackDistance = 3f;
    private float _attackCooldown = 0.5f;
    private float _timeLeft = 0;
    private int _damage = 1;

    private void Awake()
    {
        _thirdPersonRotation = GetComponent<ThirdPersonRotation>();
    }

    public void Init(TargetsProvider targetsProvider)
    {
        _targetsProvider = targetsProvider;
    }

    private void Update()
    {
        _timeLeft += Time.deltaTime;

        if (_targetsProvider.TryGetNearest(transform.position, _attackDistance, out Target target) == false)
        {
            return;
        }

        _thirdPersonRotation.Set(target);

        if (_timeLeft >= _attackCooldown)
        {
            Debug.Log(target);
            _timeLeft = 0;
            target.TakeDamage(_damage);
        }
    }
}
