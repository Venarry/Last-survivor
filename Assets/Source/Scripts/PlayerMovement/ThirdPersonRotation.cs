using UnityEngine;

public class ThirdPersonRotation : MonoBehaviour
{
    [SerializeField] private float _speed = 0.12f;
    private Vector3 _moveDirection;
    private IInputProvider _inputProvider;
    private Target _target;

    public Quaternion Rotation => transform.rotation;

    public void Init(IInputProvider inputProvider)
    {
        _inputProvider = inputProvider;
    }

    private void Update()
    {
        if(_target == null)
        {
            Set(_inputProvider.MoveDirection);
            RotateToMovement();
        }
        else
        {
            Rotate(_target.Position);
        }
    }

    public void Set(Target target)
    {
        _target = target;
    }

    private void Set(Vector3 moveDirection)
    {
        _moveDirection.x = moveDirection.x * Time.deltaTime;
        _moveDirection.z = moveDirection.z * Time.deltaTime;
        _moveDirection.y = 0;
    }

    private void RotateToMovement()
    {
        _moveDirection = _moveDirection.normalized;

        if (_moveDirection.magnitude == 0) 
            return;

        Rotate(_moveDirection);
    }

    private void Rotate(Vector3 direction)
    {
        Quaternion rotateDirection = Quaternion.LookRotation(direction);
        Quaternion targetRotation = Quaternion.Lerp(transform.rotation, rotateDirection, _speed);

        transform.rotation = targetRotation;
    }
}
