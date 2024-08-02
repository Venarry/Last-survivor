public class CritAttackSkill : SkillBehaviour
{
    private float _critDamageMultiplier = 1.5f;
    private float _critDamageMultiplierForLevel = 0.5f;

    private float _critChance = 30;
    private float _critChanceForLevel = 5;

    private PlayerAttackHandler _playerAttackHandler;

    public CritAttackSkill(PlayerAttackHandler playerAttackHandler)
    {
        _playerAttackHandler = playerAttackHandler;
        _playerAttackHandler.AttackBegin += OnAttackBegin;
    }

    ~CritAttackSkill()
    {
        _playerAttackHandler.AttackBegin -= OnAttackBegin;
    }

    public override SkillTickType SkillTickType => SkillTickType.HasNoTick;
    public override bool HasCooldown => true;

    protected override void OnLevelAdd()
    {
        _critDamageMultiplier += _critDamageMultiplierForLevel;
        _critChance += _critChanceForLevel;
    }

    private void OnAttackBegin(Target target, int damage)
    {
        if (TryAttackWithCrit() == true)
        {
            _playerAttackHandler.TryAttackWithResetTimeLeft(target, damage * _critDamageMultiplier);
        }
    }

    private bool TryAttackWithCrit()
    {
        int roll = UnityEngine.Random.Range(0, 101);

        return _critChance >= roll;
    }
}