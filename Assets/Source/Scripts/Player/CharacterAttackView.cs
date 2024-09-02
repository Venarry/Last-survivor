using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterAttackHandler))]
public class CharacterAttackView : MonoBehaviour
{
    [SerializeField] private PlayerWeapon _swordPrefab;
    [SerializeField] private PlayerWeapon _axePrefab;
    [SerializeField] private PlayerWeapon _pickaxePrefab;

    private Dictionary<TargetType, Func<float, PlayerWeapon>> _playerViewAttackTypes;
    private CharacterAttackHandler _characterAttackHandler;

    private void Awake()
    {
        _characterAttackHandler = GetComponent<CharacterAttackHandler>();

        _playerViewAttackTypes = new()
        {
            { TargetType.Enemy, AttackEnemy },
            { TargetType.Wood, AttackWood },
            { TargetType.Ore, AttackOre },
        };
    }

    private void OnEnable()
    {
        _characterAttackHandler.AttackBegin += OnAttackBegin;
    }

    private void OnDisable()
    {
        _characterAttackHandler.AttackBegin -= OnAttackBegin;
    }

    private void OnAttackBegin(Target target, float attackDelay)
    {
        PlayerWeapon weapon = _playerViewAttackTypes[target.TargetType](attackDelay);
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
