using System.Collections.Generic;
using UnityEngine;

public class TargetsProvider<T> where T : MonoBehaviour
{
    private readonly List<T> _targets = new();
    private readonly RaycastHit[] _raycastHits = new RaycastHit[32];

    public T[] Targets => _targets.ToArray();

    public void Add(T target)
    {
        if (_targets.Contains(target) == true)
            return;

        _targets.Add(target);
    }

    public void Remove(T target)
    {
        if (_targets.Contains(target) == false)
            return;

        _targets.Remove(target);
    }

    public T[] GetAll() => _targets.ToArray();

    public bool TryGetNearest(Vector3 position, float maxDistance, out T nearestTarget)
    {
        nearestTarget = default;
        float nearestDistance = Mathf.Infinity;

        foreach (T currentTarget in _targets)
        {
            float distance = Vector3.Distance(currentTarget.transform.position, position);

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

    public bool TryGetRayTargets(Ray ray, float distance, out T[] targets)
    {
        targets = new T[0];
        List<T> listTargets = new();
        int hitsCount = Physics.RaycastNonAlloc(ray, _raycastHits, distance);

        if (hitsCount == 0)
        {
            return false;
        }

        for (int i = 0; i < hitsCount; i++)
        {
            if (_raycastHits[i].collider.TryGetComponent(out T target))
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
