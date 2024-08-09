using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Loot : MonoBehaviour, IPoolObject<Loot>
{
    private const float MoveToPlayerDelay = 1.5f;

    private readonly WaitForSeconds _waitForPickUp = new(MoveToPlayerDelay);
    private Rigidbody _rigidbody;
    private int _reward;
    private int _experienceReward;
    private LootType _lootType;
    private ILootHolder _lootHolder;

    public event Action<Loot> LifeCycleEnded;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Init(int reward, int experience, LootType lootType, ILootHolder lootHolder)
    {
        _reward = reward;
        _experienceReward = experience;
        _lootType = lootType;
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

        _lootHolder.Add(_lootType, _reward);
        _lootHolder.Add(_experienceReward);
        //Destroy(gameObject);
        LifeCycleEnded?.Invoke(this);
    }

    public void Respawn(Vector3 spawnPosition, Quaternion rotation)
    {
        transform.position = spawnPosition;
        transform.rotation = rotation;
    }

    public void ResetSettings()
    {
        _rigidbody.useGravity = true;
    }
}
