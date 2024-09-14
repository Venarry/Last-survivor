public class CritAttackSkill : SkillBehaviour
{
    private readonly CharacterBuffsModel _characterBuffsModel;
    private readonly CritDamageBuff _critDamageBuff = new();

    private readonly float _critDamageMultiplierPerLevel = 0.2f;
    private readonly float _critChancePerLevel = 10;

    private readonly float _baseCritDamageMultiplier = 1.3f;
    private readonly float _baseCritChance = 20;

    private float CritDamage => _baseCritDamageMultiplier + _critDamageMultiplierPerLevel * (CurrentLevel - 1);
    private float CritChance => _baseCritChance + _critChancePerLevel * (CurrentLevel - 1);

    public CritAttackSkill(CharacterBuffsModel characterBuffsModel)
    {
        _characterBuffsModel = characterBuffsModel;
    }

    public override UpgradeType UpgradeType => UpgradeType.CritAttack;
    public override SkillTickType SkillTickType => SkillTickType.AwakeTick;
    public override bool HasCooldown => false;

    public override void Apply()
    {
        _characterBuffsModel.Add(_critDamageBuff);
    }

    public override void Disable()
    {
        _characterBuffsModel.Remove(_critDamageBuff);
    }

    protected override void OnLevelChange()
    {
        _critDamageBuff.SetParameters(CritDamage, CritChance);
    }

    public override string GetUpLevelDescription() 
    {
        string critDamageUpgradeText = "";
        string critChanceUpgradeText = "";

        if (CurrentLevel > 0)
        {
            critDamageUpgradeText = $"(+{GameParamenters.TextColorStart}{_critDamageMultiplierPerLevel * 100}%{GameParamenters.TextColorEnd})";
            critChanceUpgradeText = $"(+{GameParamenters.TextColorStart}{_critChancePerLevel}%{GameParamenters.TextColorEnd})";
        }

        return $"Crit damage {CritDamage * 100}% {critDamageUpgradeText}\n" +
            $"Crit chance {CritChance}% {critChanceUpgradeText}";
    }
        
}