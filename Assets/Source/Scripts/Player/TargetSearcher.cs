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
        if (TryGetNearestTarget(out Target target) == false)
        {
            return false;
        }

        return true;
    }

    public bool TryGetNearestTarget(out Target target) =>
        _targetsProvider.TryGetNearest(transform.position, _attackDistance, out target);
}