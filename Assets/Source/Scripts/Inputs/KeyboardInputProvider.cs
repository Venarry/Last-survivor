using UnityEngine;

public class KeyboardInputProvider : IInputProvider
{
    private const string VectorHorizontal = "Horizontal";
    private const string VectorVertical = "Vertical";

    public Vector3 MoveDirection =>
        new Vector3(Input.GetAxisRaw(VectorHorizontal), 0, Input.GetAxisRaw(VectorVertical)).normalized;
}
