﻿using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Loot : MonoBehaviour
{
    private const float MoveToPlayerDelay = 1f;
    private readonly WaitForSeconds _waitForSeconds = new(MoveToPlayerDelay);
    private Rigidbody _rigidbody;
    private int _reward;
    private LootType _lootType;
    private ILootHolder _lootHolder;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();

        StartCoroutine(GoToPlayer());
    }

    public void Init(int reward, LootType lootType, ILootHolder lootHolder)
    {
        _reward = reward;
        _lootType = lootType;
        _lootHolder = lootHolder;
    }

    public void AddForce(Vector3 forceDirection, float forceStrength)
    {
        _rigidbody.AddForce(forceDirection * forceStrength);
    }

    private IEnumerator GoToPlayer()
    {
        yield return _waitForSeconds;

        float pickupDistance = 1f;

        while(Vector3.Distance(_lootHolder.Position, transform.position) > pickupDistance)
        {
            _lootHolder.Add(_lootType, _reward);
            Destroy(gameObject);
        }
    }
}
