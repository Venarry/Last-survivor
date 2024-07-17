using UnityEngine;

[RequireComponent(typeof(ThirdPersonMovement))]
[RequireComponent(typeof(ThirdPersonRotation))]
public class Player : MonoBehaviour
{
    private ThirdPersonMovement _thirdPersonMovement;
    private ThirdPersonRotation _thirdPersonRotation;

    private void Awake()
    {
        _thirdPersonMovement = GetComponent<ThirdPersonMovement>();
        _thirdPersonRotation = GetComponent<ThirdPersonRotation>();
    }

    public void Init(IInputProvider inputProvider)
    {
        _thirdPersonMovement.Init(inputProvider);
        _thirdPersonRotation.Init(inputProvider);
    }
}
