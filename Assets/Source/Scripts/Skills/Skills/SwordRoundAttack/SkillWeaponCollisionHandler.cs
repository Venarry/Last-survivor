using UnityEngine;

public class SkillWeaponCollisionHandler : MonoBehaviour
{
    private CharacterAttackParameters _characterAttackParameters;
    private float _damageMultiplier = 0.6f;

    public void Init(CharacterAttackParameters characterAttackParameters, float damageMultiplier)
    {
        _characterAttackParameters = characterAttackParameters;
        _damageMultiplier = damageMultiplier;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Target target))
        {
            if(target.IsFriendly == false)
            {
                float damage = _characterAttackParameters.GetDamage(target.TargetType);
                target.TakeDamage(damage * _damageMultiplier);
            }
        }
    }
}
