public class DamageForEnemyUpgrade : ParametersUpgradeBehaviour
{
    private readonly CharacterAttackParameters _characterAttackParameters;
    private readonly float _damagePerLevel = 0.3f;

    public DamageForEnemyUpgrade(CharacterAttackParameters characterAttackParameters)
    {
        _characterAttackParameters = characterAttackParameters;
    }

    protected override void OnLevelAdd()
    {
        _characterAttackParameters.EnemyDamage += _damagePerLevel;
    }

    public override void Disable()
    {
        _characterAttackParameters.EnemyDamage -= _damagePerLevel * CurrentLevel;
    }

    public override string GetUpLevelDescription()
    {
        return "";
    }
}