using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterAttackHandler : MonoBehaviour
{
    [SerializeField] private PlayerWeapon _swordPrefab;
    [SerializeField] private PlayerWeapon _axePrefab;
    [SerializeField] private PlayerWeapon _pickaxePrefab;

    private float _timeLeft = 0;
    private CharacterAttackParameters _characterAttackParameters;
    private CharacterBuffsModel _characterBuffsModel;
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

    public void Init(CharacterAttackParameters characterAttackParameters, CharacterBuffsModel characterBuffsModel)
    {
        _characterAttackParameters = characterAttackParameters;
        _characterBuffsModel = characterBuffsModel;

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

        ICritDamageBuff[] critDamageBuffs = _characterBuffsModel.GetBuffs<ICritDamageBuff>();
        critDamageBuffs = critDamageBuffs.OrderBy(c => c.DamageMultiplier).ToArray();
        float buffedDamage = damage;

        foreach (ICritDamageBuff buff in critDamageBuffs)
        {
            if(buff.TryGetCrit(damage, out buffedDamage) == true)
            {
                break;
            }
        }

        target.TakeDamage(buffedDamage);
        _timeLeft = 0;

        AttackEnd?.Invoke(target, damage);
        _activeAttack = null;
    }

    private PlayerWeapon AttackEnemy(float duration) => CreateWeapon(_swordPrefab, duration);

    private PlayerWeapon AttackWood(float duration) => CreateWeapon(_axePrefab, duration);

    private PlayerWeapon AttackOre(float duration) => CreateWeapon(_pickaxePrefab, duration);

    private PlayerWeapon CreateWeapon(PlayerWeapon prefab, float duration)
    {
        PlayerWeapon playerWeapon = Instantiate(prefab, transform.position, Quaternion.identity);
        playerWeapon.Init(duration, transform);

        return playerWeapon;
    }
}
