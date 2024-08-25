using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttackHandler : MonoBehaviour
{
    [SerializeField] private PlayerWeapon _swordPrefab;
    [SerializeField] private PlayerWeapon _axePrefab;
    [SerializeField] private PlayerWeapon _pickaxePrefab;

    private float _timeLeft = 0;
    private CharacterAttackParameters _characterAttackParameters;
    private Dictionary<TargetType, Func<float, PlayerWeapon>> _playerViewAttackTypes;
    private Coroutine _activeAttack;

    public event Action<float> AttackBegin;
    public event Action<Target, float> AttackEnd;

    private void Awake()
    {
        _playerViewAttackTypes = new()
        {
            { TargetType.Enemy, AttackEnemy },
            { TargetType.Wood, AttackWood },
            { TargetType.Ore, AttackOre },
        };
    }

    public void Init(CharacterAttackParameters characterAttackParameters)
    {
        _characterAttackParameters = characterAttackParameters;
        _timeLeft = _characterAttackParameters.AttackCooldown;
    }

    private void Update()
    {
        _timeLeft += Time.deltaTime;
    }

    public void TryAttack(Target target)
    {
        if (target == null)
            return;

        float attackCooldown = _characterAttackParameters.AttackCooldown;

        if(_activeAttack != null)
        {
            return;
        }

        if (_timeLeft >= attackCooldown)
        {
            float damage = _characterAttackParameters.GetDamage(target.TargetType);

            _activeAttack = StartCoroutine(AttackWithResetTimeLeft(target, damage));
        }
    }

    public IEnumerator AttackWithResetTimeLeft(Target target, float damage)
    {
        float attackDelay = _characterAttackParameters.AttackDelay;
        PlayerWeapon weapon = _playerViewAttackTypes[target.TargetType](attackDelay);
        AttackBegin?.Invoke(attackDelay);

        yield return new WaitForSeconds(attackDelay);

        target.TakeDamage(damage);
        _timeLeft = 0;

        AttackEnd?.Invoke(target, damage);
        _activeAttack = null;
    }

    private PlayerWeapon AttackEnemy(float duration) => CreateWeapon(_axePrefab, duration);

    private PlayerWeapon AttackWood(float duration) => CreateWeapon(_axePrefab, duration);

    private PlayerWeapon AttackOre(float duration) => CreateWeapon(_pickaxePrefab, duration);

    private PlayerWeapon CreateWeapon(PlayerWeapon prefab, float duration)
    {
        PlayerWeapon playerWeapon = Instantiate(prefab, transform.position, Quaternion.identity);
        playerWeapon.Init(duration, transform);

        return playerWeapon;
    }
}