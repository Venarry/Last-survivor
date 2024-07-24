using UnityEngine;

public class TargetSearcher : MonoBehaviour
{
    private TargetsProvider _targetsProvider;
    private float _attackDistance = 3f;

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