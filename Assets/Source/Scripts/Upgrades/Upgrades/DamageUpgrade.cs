public abstract class DamageUpgrade : ParametersUpgradeBehaviour
{
    protected DamageUpgrade(CharacterBuffsModel characterBuffsModel) : base(characterBuffsModel)
    {
    }

    protected abstract DamageBuff DamageBuff { get; }
    protected virtual float DamagePerLevel { get; } = 0.1f;
    protected abstract string TargetName { get; }
    private float Damage => DamagePerLevel * CurrentLevel;

    public override void Apply()
    {
        CharacterBuffsModel.Add(DamageBuff);
        DamageBuff.SetParameters(Damage);
    }

    protected override void OnLevelChange()
    {
        DamageBuff.SetParameters(Damage);
    }

    public override void Disable()
    {
        CharacterBuffsModel.Remove(DamageBuff);
    }

    public override string GetUpLevelDescription()
    {
        string description = $"Additional damage for {TargetName}:\n{CurrentLevel * DamagePerLevel} + {Decorate(DamagePerLevel.ToString())}";

        return description;
    }
}
