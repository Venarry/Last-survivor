using UnityEngine;

[RequireComponent(typeof(ThirdPersonMovement))]
public class Player : MonoBehaviour
{
    private ThirdPersonMovement _thirdPersonMovement;

    private void Awake()
    {
        _thirdPersonMovement = GetComponent<ThirdPersonMovement>();
    }

    public void Init(IInputProvider inputProvider)
    {
        _thirdPersonMovement.Init(inputProvider);
    }
}
