public class DamageForOreUpgrade : ParametersUpgradeBehaviour
{
    private readonly CharacterAttackParameters _characterAttackParameters;
    private readonly float _damagePerLevel = 0.3f;

    public DamageForOreUpgrade(CharacterAttackParameters characterAttackParameters)
    {
        _characterAttackParameters = characterAttackParameters;
    }

    protected override void OnLevelAdd()
    {
        _characterAttackParameters.OreDamage += _damagePerLevel;
    }

    public override void Disable()
    {
        _characterAttackParameters.OreDamage -= _damagePerLevel * CurrentLevel;
    }

    public override string GetUpLevelDescription()
    {
        return "";
    }
}