using UnityEngine;

public class PlayerTargetSearchHandler : MonoBehaviour
{
    private TargetsProvider _targetsProvider;
    private ThirdPersonRotation _thirdPersonRotation;
    private PlayerAttackHandler _playerAttackHandler;
    private IPlayerAttackStateSwitcher _playerAttackStateSwitcher;
    private float _attackDistance = 3f;

    private void Awake()
    {
        _thirdPersonRotation = GetComponent<ThirdPersonRotation>();
        _playerAttackHandler = GetComponent<PlayerAttackHandler>();
    }

    public void Init(
        TargetsProvider targetsProvider,
        IPlayerAttackStateSwitcher playerAttackStateSwitcher)
    {
        _targetsProvider = targetsProvider;
        _playerAttackStateSwitcher = playerAttackStateSwitcher;
    }

    public void TrySetAttackState()
    {
        if (TrySearchTarget())
        {
            _playerAttackStateSwitcher.SetAttackState();
        }
    }

    public void TrySetSearchState()
    {
        if (HasNearestTarget(out _) == false)
        {
            _playerAttackStateSwitcher.SetTargetSearchState();
        }
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