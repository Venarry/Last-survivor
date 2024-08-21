public class DamageForWoodUpgrade : UpgradeBehaviour
{
    private readonly CharacterAttackParameters _characterAttackParameters;
    private readonly float _damagePerLevel = 0.3f;

    public DamageForWoodUpgrade(CharacterAttackParameters characterAttackParameters)
    {
        _characterAttackParameters = characterAttackParameters;
    }

    public override void Cancel()
    {
        _characterAttackParameters.WoodDamage -= _damagePerLevel * CurrentLevel;
    }

    protected override void OnApply()
    {
        _characterAttackParameters.WoodDamage += _damagePerLevel;
    }
}
