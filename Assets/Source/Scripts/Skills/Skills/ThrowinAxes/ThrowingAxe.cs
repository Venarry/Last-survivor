using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SkillWeaponCollisionHandler))]
public class ThrowingAxe : MonoBehaviour
{
    [SerializeField] private Transform _axesModel;

    private SkillWeaponCollisionHandler _skillSwordCollisionHandler;
    private Transform _owner;

    public event Action<ThrowingAxe> Coming;

    private void Awake()
    {
        _skillSwordCollisionHandler = GetComponent<SkillWeaponCollisionHandler>();
    }

    public void Init(float throwDistance, Transform owner, CharacterAttackParameters characterAttackParameters, float damageMultiplier)
    {
        _owner = owner;
        _skillSwordCollisionHandler.Init(characterAttackParameters, damageMultiplier);

        StartCoroutine(Throw(throwDistance));
    }

    private void Update()
    {
        float rotateAnglePerSecond = 720;

        _axesModel.Rotate(rotateAnglePerSecond * Time.deltaTime * Vector3.up);
    }

    private IEnumerator Throw(float range)
    {
        Vector3 throwPoint = transform.position + new Vector3(0, 0, range);
        float epsilon = 0.1f;
        float deltaDistance = 15f;

        while ((throwPoint - transform.position).magnitude > epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, throwPoint, deltaDistance * Time.deltaTime);

            yield return null;
        }

        while ((_owner.position - transform.position).magnitude > epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, _owner.position, deltaDistance * Time.deltaTime);

            yield return null;
        }

        Coming?.Invoke(this);
        Destroy(gameObject);
    }
}
