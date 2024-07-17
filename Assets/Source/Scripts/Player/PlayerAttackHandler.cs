using UnityEngine;

public class PlayerAttackHandler : MonoBehaviour
{
    private TargetsProvider _targetsProvider;
    private float _attackDistance = 3f;

    public void Init(TargetsProvider targetsProvider)
    {
        _targetsProvider = targetsProvider;
    }

    private void Update()
    {
        if (_targetsProvider.TryGetNearest(transform.position, _attackDistance, out ITarget target))
        {
            Debug.Log(target);
        }
    }
}
