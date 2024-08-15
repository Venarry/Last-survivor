public class UpgradesFactory
{
    private CharacterAttackParameters _characterAttackParameters;

    public UpgradesFactory(CharacterAttackParameters characterAttackParameters)
    {
        _characterAttackParameters = characterAttackParameters;
    }

    public DamageForEnemyUpgrade CreateDamageForEnemy() => new(_characterAttackParameters);
    public DamageForWoodUpgrade CreateDamageForWood() => new(_characterAttackParameters);
    public DamageForOreUpgrade CreateDamageForOre() => new(_characterAttackParameters);
}
