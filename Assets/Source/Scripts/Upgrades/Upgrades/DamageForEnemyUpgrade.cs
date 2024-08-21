public class DamageForEnemyUpgrade : UpgradeBehaviour
{
    private readonly CharacterAttackParameters _characterAttackParameters;
    private readonly float _damagePerLevel = 0.3f;

    public DamageForEnemyUpgrade(CharacterAttackParameters characterAttackParameters)
    {
        _characterAttackParameters = characterAttackParameters;
    }

    public override void Cancel()
    {
        _characterAttackParameters.EnemyDamage -= _damagePerLevel * CurrentLevel;
    }

    protected override void OnApply()
    {
        _characterAttackParameters.EnemyDamage += _damagePerLevel;
    }
}