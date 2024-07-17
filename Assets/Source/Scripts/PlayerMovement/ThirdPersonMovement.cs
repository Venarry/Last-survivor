using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class ThirdPersonMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 7f;
    
    private CharacterController _characterController;
    private Vector3 _moveDirection;
    private IInputProvider _inputProvider;

    public Vector3 Direction => _moveDirection;
    public Vector3 Position => transform.position;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    public void Init(IInputProvider inputProvider)
    {
        _inputProvider = inputProvider;
    }

    public void Update()
    {
        SetDirection(_inputProvider.MoveDirection);

        _characterController.Move(_speed * Time.deltaTime * _moveDirection);
    }

    public void SetPosition(Vector3 position)
    {
        _characterController.enabled = false;
        transform.position = position;
        _characterController.enabled = true;
    }

    private void SetDirection(Vector3 moveDirection)
    {
        _moveDirection = moveDirection;
        _moveDirection = _moveDirection.normalized;

        _moveDirection.y = -1f;
    }
}
