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

    public void SearchEnemy()
    {
        if (_targetsProvider.TryGetNearest(transform.position, _attackDistance, out Target target) == false)
        {
            return;
        }

        _thirdPersonRotation.Set(target);
        _playerAttackHandler.Set(target);
        _playerAttackStateSwitcher.SetAttackState();
    }
}