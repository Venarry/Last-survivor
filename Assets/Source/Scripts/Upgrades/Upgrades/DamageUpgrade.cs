public abstract class DamageUpgrade : ParametersUpgradeBehaviour
{
    private readonly CharacterBuffsModel _characterBuffsModel;
    private float _damage = 0;

    protected abstract DamageBuff DamageBuff { get; }
    protected virtual float DamagePerLevel { get; } = 0.1f;

    public DamageUpgrade(CharacterBuffsModel characterBuffsModel)
    {
        _characterBuffsModel = characterBuffsModel;
    }

    public override void Apply()
    {
        _characterBuffsModel.Add(DamageBuff);
    }

    protected override void OnLevelAdd()
    {
        _damage += DamagePerLevel;
        DamageBuff.SetParameters(_damage);
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
