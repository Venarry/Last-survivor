using System.Collections.Generic;
using UnityEngine;

public class TargetsProvider
{
    private readonly List<ITarget> _targets = new();

    public ITarget[] Targets => _targets.ToArray();

    public void Add(ITarget target)
    {
        if (_targets.Contains(target) == true)
            return;

        _targets.Add(target);
    }

    public void Remove(ITarget target)
    {
        if (_targets.Contains(target) == false)
            return;

        _targets.Remove(target);
    }

    public bool TryGetNearest(Vector3 position, float maxDistance, out ITarget nearestTarget)
    {
        nearestTarget = null;
        float nearestDistance = Mathf.Infinity;

        foreach (ITarget currentTarget in _targets)
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
