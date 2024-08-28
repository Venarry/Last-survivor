using UnityEngine;

public class CharacterTargetSearcher
{
    private readonly Transform _owner;
    private readonly TargetsProvider _targetsProvider;
    private float _attackDistance = 3f;

    public CharacterTargetSearcher(
        Transform owner,
        TargetsProvider targetsProvider)
    {
        _owner = owner;
        _targetsProvider = targetsProvider;
    }

    public bool TryGetNearestTarget(out Target target) =>
        _targetsProvider.TryGetNearest(_owner.position, _attackDistance, out target);

    public bool HasFrowardTarget()
    {
        Ray ray = new(_owner.position + new Vector3(0, 1f, 0), Vector3.forward);
        bool has = _targetsProvider.TryGetRayTargets(ray, _attackDistance, out Target[] targets);
        Debug.Log(targets.Length);
        return has;
    }
}