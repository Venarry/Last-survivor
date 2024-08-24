using UnityEngine;

public class CharacterAttackParameters
{
    private readonly CharacterBuffsModel _characterBuffsModel;

    public CharacterAttackParameters(CharacterBuffsModel characterBuffsModel)
    {
        _characterBuffsModel = characterBuffsModel;
    }

    private readonly float _baseEnemyDamage = 1;
    private readonly float _baseWoodDamage = 1;
    private readonly float _baseOreDamage = 1;
    private readonly float _baseAttackCooldown = 0.3f;
    private readonly float _baseAttackRange = 3;

    public float EnemyDamage => ApplyDamage(_baseEnemyDamage, TargetType.Enemy);
    public float WoodDamage => ApplyDamage(_baseWoodDamage, TargetType.Wood);
    public float OreDamage => ApplyDamage(_baseOreDamage, TargetType.Ore);
    public float AttackCooldown => ApplyAttackCooldown();
    public float AttackDelay => _baseAttackCooldown / 2;
    public float AttackRange => _baseAttackRange;

    private float ApplyDamage(float damage, TargetType targetType)
    {
        IDamageBuff[] damageBuffs = _characterBuffsModel.GetBuffs<IDamageBuff>();

        foreach (IDamageBuff buff in damageBuffs)
        {
            if(buff.TargetType == targetType)
            {
                damage = buff.ApplyDamage(damage);
            }
        }

        return damage;
    }

    private float ApplyAttackCooldown()
    {
        IAttackSpeedBuff[] attackSpeedBuffs = _characterBuffsModel.GetBuffs<IAttackSpeedBuff>();
        float attackCooldown = _baseAttackCooldown;

        foreach (IAttackSpeedBuff buff in attackSpeedBuffs)
        {
            attackCooldown = buff.ApplyCooldown(attackCooldown);
        }

        return attackCooldown;
    }
}
