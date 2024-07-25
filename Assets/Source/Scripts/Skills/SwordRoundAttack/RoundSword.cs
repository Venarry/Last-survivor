using System.Collections.Generic;
using UnityEngine;

public class RoundSword : MonoBehaviour
{
    [SerializeField] private List<RoundSwordCollisionHandler> _swords;

    private Transform _target;
    private float _duration = 2f;

    private void Awake()
    {
        Destroy(gameObject, _duration);
    }

    public void Init(CharacterAttackParameters characterAttackParameters, Transform target)
    {
        foreach (RoundSwordCollisionHandler sword in _swords)
        {
            sword.Init(characterAttackParameters);
        }

        _target = target;
    }

    private void Update()
    {
        float rotateSpeed = 360 / _duration;
        transform.position = _target.position;
        transform.Rotate(rotateSpeed * Time.deltaTime * Vector3.up);
    }
}
