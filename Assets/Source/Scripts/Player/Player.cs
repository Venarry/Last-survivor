using UnityEngine;

[RequireComponent(typeof(ThirdPersonMovement))]
[RequireComponent(typeof(ThirdPersonRotation))]
[RequireComponent(typeof(TargetSearcher))]
[RequireComponent(typeof(PlayerAttackStateMachine))]
[RequireComponent(typeof(PlayerAttackHandler))]
public class Player : MonoBehaviour
{
    private ThirdPersonMovement _thirdPersonMovement;
    private ThirdPersonRotation _thirdPersonRotation;
    private TargetSearcher _targetSearcher;
    private PlayerAttackHandler _playerAttackHandler;
    private PlayerAttackStateMachine _playerAttackStateMachine;

    private void Awake()
    {
        _thirdPersonMovement = GetComponent<ThirdPersonMovement>();
        _thirdPersonRotation = GetComponent<ThirdPersonRotation>();
        _targetSearcher = GetComponent<TargetSearcher>();
        _playerAttackHandler = GetComponent<PlayerAttackHandler>();
        _playerAttackStateMachine = GetComponent<PlayerAttackStateMachine>();

        _playerAttackStateMachine.Init(_targetSearcher, _thirdPersonRotation, _playerAttackHandler, _playerAttackStateMachine);
    }

    public void Init(IInputProvider inputProvider, TargetsProvider targetsProvider)
    {
        _thirdPersonMovement.Init(inputProvider);
        _thirdPersonRotation.Init(inputProvider);
        _targetSearcher.Init(targetsProvider);
    }
}
