using UnityEngine;

public class RoundSword : MonoBehaviour
{
    private CharacterAttackParameters _characterAttackParameters;
    private Transform _target;
    private float _rotateSpeed = 100f;
    private float _duration = 2f;

    private void Awake()
    {
        Destroy(gameObject, _duration);
    }

    public void Init(CharacterAttackParameters characterAttackParameters, Transform target)
    {
        _characterAttackParameters = characterAttackParameters;
        _target = target;
    }

    private void Update()
    {
        transform.position = _target.position;
        transform.Rotate(Vector3.up * _rotateSpeed * Time.deltaTime);
    }
}
