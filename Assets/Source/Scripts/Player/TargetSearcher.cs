using UnityEngine;

public class TargetSearcher : MonoBehaviour
{
    private TargetsProvider _targetsProvider;
    private ThirdPersonRotation _thirdPersonRotation;
    private PlayerAttackHandler _playerAttackHandler;
    private float _attackDistance = 3f;

    private void Awake()
    {
        _thirdPersonRotation = GetComponent<ThirdPersonRotation>();
        _playerAttackHandler = GetComponent<PlayerAttackHandler>();
    }

    public void Init(
        TargetsProvider targetsProvider)
    {
        _targetsProvider = targetsProvider;
    }

    public bool TrySearchTarget()
    {
        if (HasNearestTarget(out Target target) == false)
        {
            return false;
        }

        _thirdPersonRotation.Set(target);
        _playerAttackHandler.Set(target);

        return true;
    }

    public bool HasNearestTarget(out Target target) =>
        _targetsProvider.TryGetNearest(transform.position, _attackDistance, out target);
}