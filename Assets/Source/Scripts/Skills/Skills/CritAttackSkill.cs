public class CritAttackSkill : SkillBehaviour
{
    private readonly CharacterBuffsModel _characterBuffsModel;
    private readonly CritDamageBuff _critDamageBuff = new();

    private readonly float _critDamageMultiplierPerLevel = 0.2f;
    private readonly float _critChancePerLevel = 10;

    private float _critDamageMultiplier = 1.3f;
    private float _critChance = 20;

    public CritAttackSkill(CharacterBuffsModel characterBuffsModel)
    {
        _characterBuffsModel = characterBuffsModel;
    }

    public override SkillTickType SkillTickType => SkillTickType.AwakeTick;
    public override bool HasCooldown => false;

    public override void Apply()
    {
        _characterBuffsModel.Add(_critDamageBuff);
        _critDamageBuff.SetParameters(_critDamageMultiplier, _critChance);
    }

    public override void Disable()
    {
        _characterBuffsModel.Remove(_critDamageBuff);
    }

    protected override void OnLevelAdd()
    {
        if (CurrentLevel <= 1)
            return;

        _critDamageMultiplier += _critDamageMultiplierPerLevel;
        _critChance += _critChancePerLevel;

        _critDamageBuff.SetParameters(_critDamageMultiplier, _critChance);
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

        return $"Crit damage {_critDamageMultiplier * 100}% {critDamageUpgradeText}\n" +
        $"Crit chance {_critChance}% {critChanceUpgradeText}";
    }
        
}