public class DamageForWoodUpgrade : ParametersUpgradeBehaviour
{
    private readonly CharacterAttackParameters _characterAttackParameters;
    private readonly float _damagePerLevel = 0.3f;

    public DamageForWoodUpgrade(CharacterAttackParameters characterAttackParameters)
    {
        _characterAttackParameters = characterAttackParameters;
    }

    protected override void OnLevelAdd()
    {
        _characterAttackParameters.WoodDamage += _damagePerLevel;
    }

    public override void Disable()
    {
        _characterAttackParameters.WoodDamage -= _damagePerLevel * CurrentLevel;
    }

    public override string GetUpgradeDescription()
    {
        return "";
    }
}
