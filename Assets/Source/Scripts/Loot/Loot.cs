using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Loot : MonoBehaviour
{
    private const float MoveToPlayerDelay = 1f;
    private readonly WaitForSeconds _waitForSeconds = new(MoveToPlayerDelay);
    private Rigidbody _rigidbody;
    private int _reward;
    private int _experienceReward;
    private LootType _lootType;
    private ILootHolder _lootHolder;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();

        StartCoroutine(GoToPlayer());
    }

    public void Init(int reward, int experience, LootType lootType, ILootHolder lootHolder)
    {
        _reward = reward;
        _experienceReward = experience;
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
        float deltaDistance = 0.1f;

        while(Vector3.Distance(_lootHolder.ReceivingPosition, transform.position) > pickupDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, _lootHolder.ReceivingPosition, deltaDistance);
            yield return null;
        }

        _lootHolder.Add(_lootType, _reward);
        _lootHolder.Add(_experienceReward);
        Destroy(gameObject);
    }
}
