public class UpgradesFactory
{
    private CharacterAttackParameters _characterAttackParameters;

    public UpgradesFactory(CharacterAttackParameters characterAttackParameters)
    {
        _characterAttackParameters = characterAttackParameters;
    }

    public DamageForEnemyUpgrade CreateDamageForEnemy() => new(_characterAttackParameters);
    public DamageForEnemyUpgrade CreateDamageForWood() => new(_characterAttackParameters);
    public DamageForEnemyUpgrade CreateDamageForOre() => new(_characterAttackParameters);
}
