using UnityEngine;

public class CritAttackSkill : SkillBehaviour
{
    private float _critDamageMultiplier = 1.5f;
    private float _critDamageMultiplierForLevel = 0.5f;

    private float _critChance = 30;
    private float _critChanceForLevel = 5;

    private readonly PlayerAttackHandler _playerAttackHandler;

    public CritAttackSkill(PlayerAttackHandler playerAttackHandler)
    {
        _playerAttackHandler = playerAttackHandler;
    }

    public override SkillTickType SkillTickType => SkillTickType.AwakeTick;
    public override bool HasCooldown => false;

    public override void TryCast()
    {
        _playerAttackHandler.AttackBegin += OnAttackBegin;
    }

    public override void Disable()
    {
        _playerAttackHandler.AttackBegin -= OnAttackBegin;
    }

    protected override void OnLevelAdd()
    {
        _critDamageMultiplier += _critDamageMultiplierForLevel;
        _critChance += _critChanceForLevel;
    }

    private void OnAttackBegin(Target target, float damage)
    {
        if (TryAttackWithCrit() == true)
        {
            float critDamage = damage * _critDamageMultiplier;
            _playerAttackHandler.TryAttackWithResetTimeLeft(target, critDamage);
        }
    }

    private bool TryAttackWithCrit()
    {
        int roll = Random.Range(0, 101);

        return _critChance >= roll;
    }

    public override string GetUpgradeDescription() 
    {
        string critDamageUpgradeText = "";
        string critChanceUpgradeText = "";

        if (CurrentLevel > 0)
        {
            critDamageUpgradeText = $"(+{GameParamenters.TextColorStart}{_critDamageMultiplierForLevel * 100}%{GameParamenters.TextColorEnd})";
            critChanceUpgradeText = $"(+{GameParamenters.TextColorStart}{_critChanceForLevel}%{GameParamenters.TextColorEnd})";
        }

        return $"Crit damage {_critDamageMultiplier * 100}% {critDamageUpgradeText}\n" +
        $"Crit chance {_critChance}% {critChanceUpgradeText}";
    }
        
}