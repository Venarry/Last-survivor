using UnityEngine;

public class TargetFollower : MonoBehaviour
{
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _speed = 0.25f;

    private Transform _target;

    private Vector3 TargetPosition => _target.position + _offset;

    public void Set(Transform target)
    {
        _target = target;

        transform.position = TargetPosition;
    }

    private void LateUpdate()
    {
        if (_target == null)
            return;

        Vector3 lerpPosition = Vector3.Lerp(transform.position, TargetPosition, _speed * Time.deltaTime);

        transform.position = lerpPosition;
    }

    public void UpdatePosition()
    {
        if (_target == null) 
            return;

        transform.position = TargetPosition;
    }
}
