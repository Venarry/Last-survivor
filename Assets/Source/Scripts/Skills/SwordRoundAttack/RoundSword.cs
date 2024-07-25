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

    public void Init(CharacterAttackParameters characterAttackParameters, Transform target, int swordCount, float scale)
    {
        foreach (RoundSwordCollisionHandler sword in _swords)
        {
            sword.Init(characterAttackParameters);
        }

        for (int i = 0; i < swordCount; i++)
        {
            _swords[i].gameObject.SetActive(true);
            _swords[i].transform.localScale = new Vector3(scale, scale, scale);

            _swords[i].transform.rotation = Quaternion.Euler(0, 360 / swordCount * (i + 1), 0);
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
