using UnityEngine;

public class TargetFollower : MonoBehaviour
{
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _speed = 0.25f;

    private Transform _target;

    public void Set(Transform target)
    {
        _target = target;

        transform.position = _target.position + _offset;
    }

    private void LateUpdate()
    {
        if (_target == null)
            return;

        Vector3 targetPosition = Vector3.Lerp(transform.position, _target.position + _offset, _speed * Time.deltaTime);

        transform.position = targetPosition;
    }
}
