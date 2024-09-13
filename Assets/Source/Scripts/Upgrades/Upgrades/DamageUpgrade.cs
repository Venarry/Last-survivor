public abstract class DamageUpgrade : ParametersUpgradeBehaviour
{
    private readonly CharacterBuffsModel _characterBuffsModel;

    protected abstract DamageBuff DamageBuff { get; }
    protected virtual float DamagePerLevel { get; } = 0.1f;
    private float Damage => DamagePerLevel * CurrentLevel;

    public DamageUpgrade(CharacterBuffsModel characterBuffsModel)
    {
        _characterBuffsModel = characterBuffsModel;
    }

    public override void Apply()
    {
        _characterBuffsModel.Add(DamageBuff);
    }

    protected override void OnLevelChange()
    {
        DamageBuff.SetParameters(Damage);
    }

    public override void Disable()
    {
        _characterBuffsModel.Remove(DamageBuff);
    }

    public override string GetUpLevelDescription()
    {
        return "";
    }
}
