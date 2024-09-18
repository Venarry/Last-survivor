using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Loot : MonoBehaviour, IPoolObject<Loot>
{
    private const float MoveToPlayerDelay = 1.5f;

    private readonly WaitForSeconds _waitForPickUp = new(MoveToPlayerDelay);
    private Rigidbody _rigidbody;
    private int _reward;
    private float _experienceReward;
    private ILootHolder _lootHolder;

    public abstract LootType LootType { get; }

    public event Action<Loot> LifeCycleEnded;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Init(int reward, float experience, ILootHolder lootHolder)
    {
        _reward = reward;
        _experienceReward = experience;
        _lootHolder = lootHolder;
    }

    public void GoToPlayer()
    {
        StartCoroutine(MovingToPlayer());
    }

    public void AddForce(Vector3 forceDirection, float forceStrength)
    {
        _rigidbody.AddForce(forceDirection * forceStrength);
    }

    private IEnumerator MovingToPlayer()
    {
        yield return _waitForPickUp;

        _rigidbody.useGravity = false;
        float pickupDistance = 1f;
        float deltaDistance = 50f;

        while(Vector3.Distance(_lootHolder.ReceivingPosition, transform.position) > pickupDistance)
        {
            transform.position = Vector3
                .MoveTowards(transform.position, _lootHolder.ReceivingPosition, deltaDistance * Time.deltaTime);
            yield return null;
        }

        _lootHolder.Add(LootType, _reward);
        _lootHolder.Add(_experienceReward);

        LifeCycleEnded?.Invoke(this);
    }

    public void PlaceInPool()
    {
        LifeCycleEnded?.Invoke(this);
    }

    public void Respawn(Vector3 spawnPosition, Quaternion rotation)
    {
        transform.position = spawnPosition;
        transform.rotation = rotation;
    }

    public void ResetSettings(int reward, float experienceReward)
    {
        _rigidbody.useGravity = true;
        _reward = reward;
        _experienceReward = experienceReward;
    }
}