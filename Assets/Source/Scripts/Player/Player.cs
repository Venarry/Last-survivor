using UnityEngine;

[RequireComponent(typeof(ThirdPersonMovement))]
[RequireComponent(typeof(ThirdPersonRotation))]
[RequireComponent(typeof(PlayerTargetSearcher))]
[RequireComponent(typeof(PlayerAttackStateMachine))]
[RequireComponent(typeof(PlayerAttackHandler))]
public class Player : MonoBehaviour
{
    private ThirdPersonMovement _thirdPersonMovement;
    private ThirdPersonRotation _thirdPersonRotation;
    private PlayerTargetSearcher _playerTargetSearcher;
    private PlayerAttackHandler _playerAttackHandler;
    private PlayerAttackStateMachine _playerAttackStateMachine;

    private void Awake()
    {
        _thirdPersonMovement = GetComponent<ThirdPersonMovement>();
        _thirdPersonRotation = GetComponent<ThirdPersonRotation>();
        _playerTargetSearcher = GetComponent<PlayerTargetSearcher>();
        _playerAttackHandler = GetComponent<PlayerAttackHandler>();
        _playerAttackStateMachine = GetComponent<PlayerAttackStateMachine>();

        PlayerAttackStateMachineTransitions playerAttackStateMachineTransitions = new(_playerTargetSearcher, _playerAttackStateMachine);

        PlayerTargetSearchState playerTargetSearchState = new(playerAttackStateMachineTransitions);
        PlayerAttackState playerAttackState = new(
            _thirdPersonRotation, _playerAttackHandler, _playerTargetSearcher, playerAttackStateMachineTransitions);

        _playerAttackStateMachine.Register(playerTargetSearchState);
        _playerAttackStateMachine.Register(playerAttackState);

        _playerAttackStateMachine.SetTargetSearchState();
    }

    public void Init(IInputProvider inputProvider, TargetsProvider targetsProvider)
    {
        _thirdPersonMovement.Init(inputProvider);
        _thirdPersonRotation.Init(inputProvider);
        _playerTargetSearcher.Init(targetsProvider);
    }
}
