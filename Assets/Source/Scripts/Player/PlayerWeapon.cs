using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    private float _rotateAngle = 180;
    private float _rotateSpeed;
    private Transform _target;

    public void Init(float duration, Transform target)
    {
        _rotateSpeed = _rotateAngle / duration;
        transform.rotation = Quaternion.Euler(0, _rotateAngle / 2 + target.rotation.eulerAngles.y, 0);
        _target = target;

        Destroy(gameObject, duration);
    }

    private void Update()
    {
        transform.Rotate(-_rotateSpeed * Time.deltaTime * Vector3.up);
        transform.position = _target.position;
    }
}
