using UnityEngine;

[RequireComponent(typeof(ThirdPersonMovement))]
[RequireComponent(typeof(ThirdPersonRotation))]
[RequireComponent(typeof(PlayerTargetSearchHandler))]
[RequireComponent(typeof(PlayerAttackStateMachine))]
[RequireComponent(typeof(PlayerAttackHandler))]
public class Player : MonoBehaviour
{
    private ThirdPersonMovement _thirdPersonMovement;
    private ThirdPersonRotation _thirdPersonRotation;
    private PlayerTargetSearchHandler _playerTargetSearchHandler;
    private PlayerAttackHandler _playerAttackHandler;
    private PlayerAttackStateMachine _playerAttackStateMachine;

    private void Awake()
    {
        _thirdPersonMovement = GetComponent<ThirdPersonMovement>();
        _thirdPersonRotation = GetComponent<ThirdPersonRotation>();
        _playerTargetSearchHandler = GetComponent<PlayerTargetSearchHandler>();
        _playerAttackHandler = GetComponent<PlayerAttackHandler>();
        _playerAttackStateMachine = GetComponent<PlayerAttackStateMachine>();

        PlayerTargetSearchState playerTargetSearchState = new(_thirdPersonRotation, _playerTargetSearchHandler);
        PlayerAttackState playerAttackState = new(_thirdPersonRotation, _playerAttackHandler);

        _playerAttackStateMachine.Register(playerTargetSearchState);
        _playerAttackStateMachine.Register(playerAttackState);

        _playerAttackStateMachine.SetTargetSearchState();
    }

    public void Init(IInputProvider inputProvider, TargetsProvider targetsProvider)
    {
        _thirdPersonMovement.Init(inputProvider);
        _thirdPersonRotation.Init(inputProvider);
        _playerTargetSearchHandler.Init(targetsProvider, _playerAttackStateMachine);
    }
}
