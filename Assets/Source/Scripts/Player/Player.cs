using UnityEngine;

[RequireComponent(typeof(ThirdPersonMovement))]
[RequireComponent(typeof(ThirdPersonRotation))]
[RequireComponent(typeof(PlayerAttackHandler))]
public class Player : MonoBehaviour
{
    private ThirdPersonMovement _thirdPersonMovement;
    private ThirdPersonRotation _thirdPersonRotation;
    private PlayerAttackHandler _playerAttackHandler;

    private void Awake()
    {
        _thirdPersonMovement = GetComponent<ThirdPersonMovement>();
        _thirdPersonRotation = GetComponent<ThirdPersonRotation>();
        _playerAttackHandler = GetComponent<PlayerAttackHandler>();
    }

    public void Init(IInputProvider inputProvider, TargetsProvider targetsProvider)
    {
        _thirdPersonMovement.Init(inputProvider);
        _thirdPersonRotation.Init(inputProvider);
        _playerAttackHandler.Init(targetsProvider);
    }
}
