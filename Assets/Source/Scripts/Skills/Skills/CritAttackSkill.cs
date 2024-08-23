using UnityEngine;

public class CritAttackSkill : SkillBehaviour
{
    private float _critDamageMultiplier = 1.5f;
    private float _critDamageMultiplierPerLevel = 0.1f;

    private float _critChance = 30;
    private float _critChancePerLevel = 5;

    private readonly PlayerAttackHandler _playerAttackHandler;

    public CritAttackSkill(PlayerAttackHandler playerAttackHandler)
    {
        _playerAttackHandler = playerAttackHandler;
    }

    public override SkillTickType SkillTickType => SkillTickType.AwakeTick;
    public override bool HasCooldown => false;

    public override void Apply()
    {
        _playerAttackHandler.AttackBegin += OnAttackBegin;
    }

    public override void Disable()
    {
        _playerAttackHandler.AttackBegin -= OnAttackBegin;
    }

    protected override void OnLevelAdd()
    {
        if (CurrentLevel <= 1)
            return;

        _critDamageMultiplier += _critDamageMultiplierPerLevel;
        _critChance += _critChancePerLevel;
    }

    private void OnAttackBegin(Target target, float damage)
    {
        if (TryAttackWithCrit() == true)
        {
            float critDamage = damage * _critDamageMultiplier;
            _playerAttackHandler.AttackWithResetTimeLeft(target, critDamage);
        }
    }

    private bool TryAttackWithCrit()
    {
        int roll = Random.Range(0, 101);

        return _critChance >= roll;
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