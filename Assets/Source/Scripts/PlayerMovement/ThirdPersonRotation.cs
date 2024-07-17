using UnityEngine;

public class ThirdPersonRotation : MonoBehaviour
{
    [SerializeField] private float _speed = 0.12f;
    private Vector3 _moveDirection;
    private IInputProvider _inputProvider;

    public Quaternion Rotation => transform.rotation;

    public void Init(IInputProvider inputProvider)
    {
        _inputProvider = inputProvider;
    }

    private void Update()
    {
        SetDirection(_inputProvider.MoveDirection);
    }

    public void SetDirection(Vector3 moveDirection)
    {
        _moveDirection.x = moveDirection.x * Time.deltaTime;
        _moveDirection.z = moveDirection.z * Time.deltaTime;
        _moveDirection.y = 0;
    }

    public void Rotate()
    {
        _moveDirection = _moveDirection.normalized;

        if (_moveDirection.magnitude == 0) 
            return;
        
        Quaternion rotateDirection = Quaternion.LookRotation(_moveDirection);
        Quaternion targetRotation = Quaternion.Lerp(transform.rotation, rotateDirection, _speed);

        transform.rotation = targetRotation;
    }
}
