using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundSwordCollisionHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out IDamageable damageable))
        {
            if(damageable.IsFriendly == false)
            {
                damageable.TakeDamage(5);
            }
        }
    }
}
