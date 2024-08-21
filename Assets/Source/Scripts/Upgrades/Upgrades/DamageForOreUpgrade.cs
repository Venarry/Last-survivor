
using UnityEngine;

public class DamageForOreUpgrade : UpgradeBehaviour
{
    private readonly CharacterAttackParameters _characterAttackParameters;
    private readonly float _damagePerLevel = 0.3f;

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
    }
}