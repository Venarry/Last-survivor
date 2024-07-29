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
            Rotate(_target.Position - transform.position);
        }
    }

    public void Set(Target target)
    {
        _target = target;
    }

    public void RemoveTarget()
    {
        _target = null;
    }

    private void Set(Vector3 moveDirection)
    {
        _moveDirection.x = moveDirection.x;
        _moveDirection.z = moveDirection.z;
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
        rotateDirection.x = 0;
        Quaternion targetRotation = Quaternion.Lerp(transform.rotation, rotateDirection, _speed * Time.deltaTime);

        transform.rotation = targetRotation;
    }
}
