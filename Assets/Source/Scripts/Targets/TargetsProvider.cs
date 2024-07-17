using System.Collections.Generic;
using UnityEngine;

public class TargetsProvider
{
    private readonly List<Target> _targets = new();

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
}
