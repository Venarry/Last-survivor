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

        PlayerAttackStateMachineTransitions playerAttackStateMachineTransitions = new(_targetSearcher, _playerAttackStateMachine);

        PlayerTargetSearchState playerTargetSearchState = new(playerAttackStateMachineTransitions);
        PlayerAttackState playerAttackState = new(
            _thirdPersonRotation, _playerAttackHandler, _targetSearcher, playerAttackStateMachineTransitions);

        _playerAttackStateMachine.Register(playerTargetSearchState);
        _playerAttackStateMachine.Register(playerAttackState);

        _playerAttackStateMachine.SetTargetSearchState();
    }

    public void Init(IInputProvider inputProvider, TargetsProvider targetsProvider)
    {
        _thirdPersonMovement.Init(inputProvider);
        _thirdPersonRotation.Init(inputProvider);
        _targetSearcher.Init(targetsProvider);
    }
}
