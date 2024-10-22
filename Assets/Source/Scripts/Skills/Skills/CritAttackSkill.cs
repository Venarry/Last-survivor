using System;
using UnityEngine;
using YG;

public class CritAttackSkill : SkillBehaviour
{
    private readonly CharacterBuffsModel _characterBuffsModel;
    private readonly CritDamageBuff _critDamageBuff = new();

    private readonly float _critDamageMultiplierPerLevel = 0.2f;
    private readonly float _critChancePerLevel = 10;

    private readonly float _baseCritDamageMultiplier = 1.3f;
    private readonly float _baseCritChance = 20;

    private float CritDamage => _baseCritDamageMultiplier + _critDamageMultiplierPerLevel * Mathf.Max(CurrentLevel - 1, 0);
    private float CritChance => _baseCritChance + _critChancePerLevel * Mathf.Max(CurrentLevel - 1, 0);

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
        _critDamageBuff.SetParameters(CritDamage, CritChance);
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
            critDamageUpgradeText = $"(+{Decorate((_critDamageMultiplierPerLevel * 100).ToString())})";
            critChanceUpgradeText = $"(+{Decorate(_critChancePerLevel.ToString())})";
        }

        string critDamageText;
        string critChanceText;

        switch (YandexGame.lang)
        {
            case GameParameters.CodeRu:
                critDamageText = "Критический урон";
                critChanceText = "Шанс крита";
                break;

            case GameParameters.CodeTr:
                critDamageText = "Kritik hasar";
                critChanceText = "Kritik şans";
                break;

            default:
                critDamageText = "Crit damage";
                critChanceText = "Crit chance";
                break;
        }

        return $"{critDamageText} {CritDamage * 100}% {critDamageUpgradeText}\n" +
            $"{critChanceText} {CritChance}% {critChanceUpgradeText}";
    }
        
}