
using UnityEngine;

public class DamageForOreUpgrade : UpgradeBehaviour
{
    private readonly CharacterAttackParameters _characterAttackParameters;
    private readonly int _damagePerLevel = 1;

    public DamageForOreUpgrade(CharacterAttackParameters characterAttackParameters)
    {
        _characterAttackParameters = characterAttackParameters;
    }

    public override void Cancel()
    {
        _characterAttackParameters.OreDamage -= _damagePerLevel * CurrentLevel;
    }

    protected override void OnApply()
    {
        _characterAttackParameters.OreDamage += _damagePerLevel;
        Debug.Log(_characterAttackParameters.OreDamage);
    }
}