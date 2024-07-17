using UnityEngine;

public class MobileInputsProvider : MonoBehaviour, IInputProvider
{
    [SerializeField] private FloatingJoystick _floatingJoystick;

    public Vector3 MoveDirection => new(_floatingJoystick.Direction.x, 0, _floatingJoystick.Direction.y);
}
