public class ParameterUpgradesFactory
{
    private readonly CharacterBuffsModel _characterBuffsModel;

    public ParameterUpgradesFactory(CharacterBuffsModel characterBuffsModel)
    {
        _characterBuffsModel = characterBuffsModel;
    }

    public DamageForEnemyUpgrade CreateDamageForEnemy() => new(_characterBuffsModel);
    public DamageForWoodUpgrade CreateDamageForWood() => new(_characterBuffsModel);
    public DamageForOreUpgrade CreateDamageForOre() => new(_characterBuffsModel);
}
