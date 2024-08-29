using System.Collections.Generic;
using UnityEngine;

public class TargetsProvider
{
    private readonly List<Target> _targets = new();
    private RaycastHit[] _raycastHits = new RaycastHit[32];

    public Target[] Targets => _targets.ToArray();

    public void Add(Target target)
    {
        if (_targets.Contains(target) == true)
            return;

        _targets.Add(target);
    }

    public void Remove(Target target)
    {
        if (_targets.Contains(target) == false)
            return;

        _targets.Remove(target);
    }

    public bool TryGetNearest(Vector3 position, float maxDistance, out Target nearestTarget)
    {
        nearestTarget = null;
        float nearestDistance = Mathf.Infinity;

        foreach (Target currentTarget in _targets)
        {
            float distance = Vector3.Distance(currentTarget.Position, position);

            if (distance > maxDistance)
                continue;

            if (distance < nearestDistance)
            {
                nearestTarget = currentTarget;
                nearestDistance = distance;
            }
        }

        return nearestTarget != null;
    }

    public bool TryGetRayTargets(Ray ray, float distance, out Target[] targets)
    {
        targets = new Target[0];
        List<Target> listTargets = new();
        int hitsCount = Physics.RaycastNonAlloc(ray, _raycastHits, distance);

        if (hitsCount == 0)
        {
            return false;
        }

        for (int i = 0; i < hitsCount; i++)
        {
            if (_raycastHits[i].collider.TryGetComponent(out Target target))
            {
                listTargets.Add(target);
            }
        }

        if(listTargets.Count == 0)
        {
            return false;
        }

        targets = listTargets.ToArray();
        return true;
    }
}
